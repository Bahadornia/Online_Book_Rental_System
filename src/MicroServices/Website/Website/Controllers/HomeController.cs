using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Website.Models;

namespace Website.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
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
    public IActionResult GetAll()
    {
        var books = new List<BookViewModel> {
            new BookViewModel{Id = Guid.NewGuid(), Author ="Babak",CategoryName="C1", ISBN=1234567890123, PublisherName="Iarnica", Title = "Book1", Description="", Image = "" },
           new BookViewModel{Id = Guid.NewGuid(), Author ="Babak",CategoryName="C1", ISBN=1234567890123, PublisherName="Iarnica", Title = "Book1", Description="", Image = "" },new BookViewModel{Id = Guid.NewGuid(), Author ="Babak",CategoryName="C1", ISBN=1234567890123, PublisherName="Iarnica", Title = "Book1", Description="", Image = "" },new BookViewModel{Id = Guid.NewGuid(), Author ="Babak",CategoryName="C1", ISBN=1234567890123, PublisherName="Iarnica", Title = "Book1", Description="", Image = "" },
        };
        var rs = new { rows = books, toatlCount = books.Count };
        return Ok(rs);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
