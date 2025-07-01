using System.Diagnostics;
using System.Threading.Tasks;
using Catalog.API.Grpc.Client.Logics;
using Catalog.API.Grpc.Client.Requests;
using Catalog.API.Grpc.Client.Responses;
using HashidsNet;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Rental.API.Grpc.Client.Logics;
using Rental.API.Grpc.Client.Requests;
using Website.Dtos;
using Website.Models;

namespace Website.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBookGrpcService _bookService;
    private readonly IRentalGrpcService _rentalGrpcService;
    private readonly IMapper _mapper;
    private readonly IHashids _hashIds;

    public HomeController(ILogger<HomeController> logger, IBookGrpcService bookService, IMapper msapper, IRentalGrpcService rentalGrpcService, IHashids hashIds)
    {
        _logger = logger;
        _bookService = bookService;
        _mapper = msapper;
        _rentalGrpcService = rentalGrpcService;
        _hashIds = hashIds;
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
        var rs = books.Select(book => new BookDto
        {
            Author = book.Author,
            Category= book.Category,
            Id = _hashIds.EncodeLong(book.Id),
            Description = book.Description,
            ISBN = book.ISBN,
            ImageUrl = book.ImageUrl,
            Publisher = book.Publisher,
            Title = book.Title
        });
        return Ok(new { rows = rs, toatlCount = books.Count });
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

    [HttpPost]
    public async Task<IActionResult> RentBook(RentBookDto dto, CancellationToken ct)
    {
        var rentBookRq = new RentBookRq
        {
            BookId = dto.BookId,
            UserId = dto.UserId,
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteBook([FromForm] string bookId, CancellationToken ct)
    {
        var id = _hashIds.DecodeLong(bookId);
       
        var deleteBookRq = new DeleteBookRq
        {
            BookId = id[0]
        };
        await _bookService.DeleteBook(deleteBookRq, ct);
        return Ok(bookId);
    }
}
