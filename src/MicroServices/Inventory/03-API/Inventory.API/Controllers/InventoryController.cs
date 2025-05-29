using Inventory.ApplicationServices.Commands;
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

        [HttpPost]
        public async Task<IActionResult> AddBookToInventory(long bookId, long initialCopies, CancellationToken ct)
        {
            return Ok();
        }

        public async Task<IActionResult> DecreaseAvailableCopies(long bookId, CancellationToken ct)
        {
            var command = new DecreaseAvailableCopiesCommand(bookId);
            await _mediator.Send(command, ct);
            return Ok();
        }
    }
}
