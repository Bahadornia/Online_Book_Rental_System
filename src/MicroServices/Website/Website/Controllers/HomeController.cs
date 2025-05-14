using System.Diagnostics;
using System.Threading.Tasks;
using Catalog.API.Grpc.Client.Logics;
using Catalog.API.Grpc.Client.Requests;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Website.Models;

namespace Website.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBookGrpcService _bookService;
    private readonly IMapper _mapper;

    public HomeController(ILogger<HomeController> logger, IBookGrpcService bookService , IMapper msapper )
    {
        _logger = logger;
        _bookService = bookService;
        _mapper = msapper;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var books = await _bookService.GetAllBooks(ct);
       
        var rs = new { rows = books, toatlCount = books.Count };
        return Ok(rs);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddBook([FromForm] BookViewModel book, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", book);
        }
        var addBookRq = _mapper.Map<AddBookRq>(book);
        await _bookService.AddBook(addBookRq, ct);
        return Created();
    }
   
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
