using Microsoft.AspNetCore.Mvc;
using Order.API.Grpc.Client.Logics;
using Order.API.Grpc.Client.Requests;

namespace Order.API.Grpc.Verification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IOrderGrpcService _rentalGrpcService;

        public RentalController(IOrderGrpcService rentalGrpcService)
        {
            _rentalGrpcService = rentalGrpcService;
        }

        [HttpPost]
        public async Task<IActionResult> BorrowBook(OrderBookRq rq , CancellationToken ct)
        {
            await _rentalGrpcService.RentBook(rq, ct);
            return Ok();
        }
    }
}
