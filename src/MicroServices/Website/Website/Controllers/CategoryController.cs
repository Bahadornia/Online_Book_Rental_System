using Catalog.API.Grpc.Client.Logics;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IHashids _hashId;

        public CategoryController(ICategoryService categoryService, IHashids hashIds)
        {
            _categoryService = categoryService;
            _hashId = hashIds;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(string term, CancellationToken ct)
        {
            var categories = await _categoryService.GetAll(term, ct);
            categories.Select(p => new
            {
                Id = _hashId.EncodeLong(p.Id),
                p.Name,
            });
            return Ok(categories);
        }
    }
}
