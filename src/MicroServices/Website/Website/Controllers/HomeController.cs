using Catalog.API.Grpc.Client.Logics;
using Catalog.API.Grpc.Client.Requests;
using HashidsNet;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Order.API.Grpc.Client.Logics;
using Order.API.Grpc.Client.Requests;
using System.Diagnostics;
using System.Globalization;
using System.Security.Claims;
using Website.Dtos;
using Website.Enum;
using Website.Models;

namespace Website.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBookGrpcService _bookService;
    private readonly IOrderGrpcService _rentalGrpcService;
    private readonly IMapper _mapper;
    private readonly IHashids _hashIds;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HttpContext _httpContext;

    public HomeController(ILogger<HomeController> logger, IBookGrpcService bookService, IMapper msapper, IOrderGrpcService rentalGrpcService, IHashids hashIds, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _bookService = bookService;
        _mapper = msapper;
        _rentalGrpcService = rentalGrpcService;
        _hashIds = hashIds;
        _httpContextAccessor = httpContextAccessor;
        _httpContext = _httpContextAccessor.HttpContext;
    }

    public IActionResult Index()
    {
        return User.IsInRole("user") ? View() : RedirectToAction("AddBook", "Book");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> GetAll([FromBody] AgGridRequestDto rq, CancellationToken ct)
    {
        IReadOnlyCollection<BookDto> rs;
        var claims = User.Claims;
        var agGridRq = _mapper.Map<AgGridRequestRq>(rq);
        var idtoken = await HttpContext.GetTokenAsync("id_token");
        var accesstoken = await HttpContext.GetTokenAsync("access_token");
      

        var result = await _bookService.GetAllBooks(agGridRq, ct);
            if(result.Books is { Count: > 0} books){
            rs = books.Select(book => new BookDto
            {
                Author = book.Author,
                Category = book.Category,
                Id = _hashIds.EncodeLong(book.Id),
                Description = book.Description,
                ISBN = book.ISBN,
                ImageUrl = book.ImageUrl,
                Publisher = book.Publisher,
                Title = book.Title
            }).ToList().AsReadOnly();
            return Ok(new { rows = rs, totalCount = result.TotalCount });
        }
        else
        {
            rs = [];
        }
            return Ok(new { rows = rs, totalCount = 0 });
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Search(BookFilterDto dto, CancellationToken ct)
    {
        var bookFilterRq = _mapper.Map<BookFilterRq>(dto);
        var books = await _bookService.SearchBook(bookFilterRq, ct);
        return Ok(new { rows = books, toatlCount = books.Count });
    }

    [HttpPost]
    public async Task<IActionResult> RentBook(RentBookDto dto, CancellationToken ct)
    {
        var userId =  _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
        var rentBookRq = new OrderBookRq
        {
            BookId = _hashIds.DecodeLong(dto.BookId)[0],
            UserId = userId,
            BorrowDate = DateTime.UtcNow,
        };
        await _rentalGrpcService.RentBook(rentBookRq);
        return Ok();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
