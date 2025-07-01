using Inventory.ApplicationServices.Commands;
using Inventory.ApplicationServices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{bookId}/add")]
        public async Task<IActionResult> AddBookToInventory(long bookId, long initialCopies, CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("{bookId}/decrease")]
        public async Task<IActionResult> DecreaseAvailableCopies(long bookId, CancellationToken ct)
        {
            var command = new DecreaseAvailableCopiesCommand(bookId);
            await _mediator.Send(command, ct);
            return Ok();
        }

        [HttpGet("{bookId}/avialable")]
        public async Task<bool> IsBookAvailable(long bookId, CancellationToken ct)
        {
            var query = new IsBookAvailableQuery(bookId);
            return await _mediator.Send(query, ct);
        }
    }
}
