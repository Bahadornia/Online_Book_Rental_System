using Catalog.API.Grpc.Client.Logics;
using Catalog.API.Grpc.Client.Requests;
using HashidsNet;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Models;

namespace Website.Controllers;

[Authorize(Roles = "admin")]
public class BookController : Controller
{
    private readonly IMapper _mapper;
    private readonly IBookGrpcService _bookService;
    private readonly IHashids _hashIds;

    public BookController(IMapper mapper, IBookGrpcService bookService, IHashids hashIds)
    {
        _mapper = mapper;
        _bookService = bookService;
        _hashIds = hashIds;
    }

    public IActionResult AddBook()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddBook([FromForm] BookViewModel book, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return View("AddBook", book);
        }
        var addBookRq = _mapper.Map<AddBookRq>(book);
        await _bookService.AddBook(addBookRq, ct);
        return Redirect("/Home/Index");
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