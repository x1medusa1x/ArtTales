using ArtTalesFull.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ArtTalesFull.Models
{
    public class UploadPostModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Headline { get; set; }

        [Required]
        public string? Description { get; set; }

        [Attachment(ErrorMessage = "Please add at least one image")]
        public List<IFormFile>? FileImages { get; set; }

        [Required]
        public int PreviewImage { get; set; }

        public int Likes { get; set; }

        public string? UserId { get; set; }



    }
}
