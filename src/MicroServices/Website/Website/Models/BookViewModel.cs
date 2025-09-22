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
        public string PublisherName { get; set; } = default!;

        public string PublisherId { get; set; } = default!;

        [Required(ErrorMessage = "دسته الزامی است.")]
        public string CategoryName { get; set; } = default!;

        public string CategoryId { get; set; } = default!;

        [Required(ErrorMessage = "شابک الزامی است.")]
        public string ISBN { get; init; } = default!;
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
