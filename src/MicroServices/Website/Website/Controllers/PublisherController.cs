using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            
            return Ok(list);
        }
    }
}
