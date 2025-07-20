using Microsoft.AspNetCore.Mvc;
using Order.API.Grpc.Client.Logics;
using Order.API.Grpc.Client.Requests;

namespace Order.API.Grpc.Verification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IRentalGrpcService _rentalGrpcService;

        public RentalController(IRentalGrpcService rentalGrpcService)
        {
            _rentalGrpcService = rentalGrpcService;
        }

        [HttpPost]
        public async Task<IActionResult> BorrowBook(RentBookRq rq , CancellationToken ct)
        {
            await _rentalGrpcService.RentBook(rq, ct);
            return Ok();
        }
    }
}
