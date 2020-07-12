using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CommunicationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TinyURLController : ControllerBase
    {
        [HttpPost]
        [Route("createURL")]
        public async Task<string> AddUrl(
            [FromForm(Name = "URL")] string url)
        {
            return url;
        }
    }
}
