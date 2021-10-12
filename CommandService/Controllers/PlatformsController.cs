namespace CommandService.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            System.Console.WriteLine("----> Inbound POST command service");
            return Ok("Inbound test from Platforms Controller");
        }
    }
}