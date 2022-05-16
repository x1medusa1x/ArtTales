using ArtTalesFull.Entities;

namespace ArtTalesFull.Models
{
    public class UploadModel : UploadPostModel
    {
        public int Id { get; set; }

        public int ArtworkId { get; set; }

        public string? ModalName { get; set; }

        public string? ModalHeadline { get; set; }

        public string? ModalDescription { get; set; }

        public List<Images>? StringImages { get; set; }

        public String? DeletedImages { get; set; }

        public String? NewStringImages { get; set; }

    }
}
