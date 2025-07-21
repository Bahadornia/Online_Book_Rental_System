using Microsoft.AspNetCore.Mvc;
using Order.API.Grpc.Client.Logics;
using Order.API.Grpc.Client.Responses;
using Website.Dtos;

namespace Website.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderGrpcService _orderService;

        public OrderController(IOrderGrpcService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var orders = await _orderService.GetAll(ct);

            var response = new GridResponseDto<IReadOnlyCollection<GetOrderRs>> { Rows = orders, TotalCount = orders.Count };
            return Ok(response);
        }
      
    }
}
