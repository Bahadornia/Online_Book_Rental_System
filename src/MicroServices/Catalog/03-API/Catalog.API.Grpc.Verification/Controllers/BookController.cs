using Catalog.API.Grpc.Client.Logics;
using Catalog.API.Grpc.Client.Requests;
using Catalog.ApplicationServices.Queries;
using Catalog.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text;

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
        public async Task<IActionResult> AddBook(string title, string author, string category, string publisher, long isbn, int availableCopies, CancellationToken ct)
        {
            var bookRq = new AddBookRq
            {
                Title = title,
                Author = author,
                Category = category,
                Publisher = publisher,
                ISBN = isbn,
                AvailableCopies = availableCopies,
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


        [HttpGet]
        public async Task<IActionResult> GetImage(string fileName, CancellationToken ct)
        {
            var rq = new GetBookImageRq { FileName = fileName };
            var url = await _bookGrpcService.GetBookImage(rq);
            return Ok(url);
        }
    }
}
