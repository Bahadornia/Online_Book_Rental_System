using System.ComponentModel.DataAnnotations;

namespace Website.Models;

public class BulkInsertViewModel
{
    [Required(ErrorMessage ="فایل الزامی است.")]
    public IFormFile File { get; set; }
}
