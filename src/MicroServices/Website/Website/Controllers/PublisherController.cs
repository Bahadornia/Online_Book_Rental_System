using Catalog.API.Grpc.Client.Logics;
using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherGrpcService _publisherService;

        public PublisherController(IPublisherGrpcService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
           var publishers = await _publisherService.GetPublishers(ct);
            return Ok(publishers);
        }
    }
}
