using Catalog.API.Grpc.Client.Logics;
using Catalog.API.Grpc.Client.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Grpc.Verification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookGrpcService _bookGrpcService;

        public BookController(IBookGrpcService bookGrpcService)
        {
            _bookGrpcService = bookGrpcService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(CancellationToken ct)
        {
            var bookRq = new AddBookRq
            {
                Author = "Babak",
                CategoryId = 1,
                PublisherId = 2,
                Description = "test",
                ISBN = 546546556456,
                Title = "re",
                Image = new byte[10]
            };
            try
            {
            await _bookGrpcService.AddBook(bookRq, ct);
            return Created();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
