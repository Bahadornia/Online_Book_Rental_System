using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Order.API.Grpc.Client.Logics;
using Order.API.Grpc.Client.Responses;
using System.Globalization;
using Website.Dtos;

namespace Website.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderGrpcService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderGrpcService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;

            var orders = await _orderService.GetAll(ct);

            var result = _mapper.Map<IReadOnlyCollection<OrderDto>>(orders);
            var response = new GridResponseDto<IReadOnlyCollection<OrderDto>> { Rows = result, TotalCount = orders.Count };
            return Ok(response);
        }
      
    }
}
