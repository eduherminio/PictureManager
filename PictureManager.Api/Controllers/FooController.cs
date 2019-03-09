using Microsoft.AspNetCore.Mvc;

namespace PictureManager.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/foo")]
    public class FooController : ControllerBase
    {
        [HttpPost]
        public string Post([FromBody] string item)
        {
            return item;
        }

        [HttpGet("{id}")]
        public string Get(string id)
        {
            return id;
        }
    }
}
