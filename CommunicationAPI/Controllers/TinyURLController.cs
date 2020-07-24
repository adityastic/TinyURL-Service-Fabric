using System;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace CommunicationAPI.Controllers
{
    [ApiController]
    [Route("/")]
    public class TinyUrlController : ControllerBase
    {
        public string GetBaseUrl()
        {
            return $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
        }

        [HttpPost]
        [Route("createURL")]
        public async Task<TinyURLModel> AddUrl(
            [FromForm(Name = "URL")] string url, [FromForm(Name = "partition")] int partition)
        {
            partition %= 3;

            var statefulProxy = ServiceProxy.Create<ITinyURLService>(
                new Uri("fabric:/TinyURL/TinyURLStatefulService"),
                new ServicePartitionKey(partition));

            var shortObj = await statefulProxy.CreateURLForService(url);

            return new TinyURLModel($"{GetBaseUrl()}/{shortObj}");
        }

        [HttpGet]
        [Route("{shortUrl}")]
        public async Task HandleUrl(
            [FromRoute(Name = "shortUrl")] string url, [FromQuery(Name = "partition")] int partition)
        {
            partition %= 3;

            var statefulProxy = ServiceProxy.Create<ITinyURLService>(
                new Uri("fabric:/TinyURL/TinyURLStatefulService"),
                new ServicePartitionKey(partition));

            HttpContext.Response.Redirect(await statefulProxy.GetURLFromShortURL(url));
        }
    }
}