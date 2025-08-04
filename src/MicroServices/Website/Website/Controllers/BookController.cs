using Catalog.API.Grpc.Client.Logics;
using Catalog.API.Grpc.Client.Requests;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class BookController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBookGrpcService _bookService;

        public BookController(IMapper mapper, IBookGrpcService bookService)
        {
            _mapper = mapper;
            _bookService = bookService;
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
    }
}
