using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Client;
using Contracts;

namespace CommunicationAPI.Controllers
{
    [ApiController]
    [Route("/")]
    public class TinyURLController : ControllerBase
    {
        [HttpPost]
        [Route("createURL")]
        public async Task<TinyURLModel> AddUrl(
            [FromForm(Name = "URL")] string url, [FromForm(Name = "partition")] int partition)
        {
            partition %= 3;

            var statefulProxy = ServiceProxy.Create<ITinyURLService>(
                new Uri("fabric:/TinyURL/TinyURLStatefulService"),
                new ServicePartitionKey(partition));

            var shortObj =  await statefulProxy.CreateURLForService(url);

            return shortObj;
        }


        [HttpGet]
        [Route("{shortUrl}")]
        public async Task<string> HandleUrl(
            [FromRoute(Name = "shortUrl")] string url, [FromQuery(Name = "partition")] int partition)
        {
            partition %= 3;

            var statefulProxy = ServiceProxy.Create<ITinyURLService>(
                new Uri("fabric:/TinyURL/TinyURLStatefulService"),
                new ServicePartitionKey(partition));

            return await statefulProxy.GetURLFromShortURL(url);
        }
    }
}
