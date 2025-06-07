using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class BookViewModel
    {
        [Required(ErrorMessage = "عنوان الزامی است.")]
        public string Title { get; init; } = default!;

        [Required(ErrorMessage = "نام نویسنده الزامی است.")]
        public string Author { get; init; } = default!;

        [Required(ErrorMessage = "ناشر الزامی است.")]
        public string Publisher { get; set; } = default!;

        [Required(ErrorMessage = "دسته الزامی است.")]
        public string Category { get; set; } = default!;

        [Required(ErrorMessage = "شابک الزامی است.")]
        public long ISBN { get; init; }
        public string? Description { get; init; }
        public IFormFile ImageFile { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public byte[] Image => ConvertsToByte(ImageFile);
        public int AvailableCopies { get; set; }
        public string ContentType => ImageFile.ContentType;

        private byte[] ConvertsToByte(IFormFile file)
        {
           
            if (file == null || file.Length == 0)
                return null;

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
