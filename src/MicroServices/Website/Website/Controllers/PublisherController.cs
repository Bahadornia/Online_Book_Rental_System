using Catalog.API.Grpc.Client.Logics;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherGrpcService _publisherService;
        private readonly IHashids _hashId;

        public PublisherController(IPublisherGrpcService publisherService, IHashids hashId)
        {
            _publisherService = publisherService;
            _hashId = hashId;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var publishers = await _publisherService.GetPublishers(ct);
            publishers.Select(p => new
            {
                Id = _hashId.EncodeLong(p.Id),
                p.Name,
            });

            return Ok(publishers);
        }
    }
}
