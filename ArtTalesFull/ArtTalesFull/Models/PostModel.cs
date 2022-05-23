using ArtTalesFull.Entities;

namespace ArtTalesFull.Models
{
    public class PostModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Headline { get; set; }

        public string? Description { get; set; }

        public List<Images>? Images { get; set; }

        public int ArtworkId { get; set; }

        public string UserId { get; set; }

        public string CurrentUser { get; set; }

        public int Likes { get; set; }

    }
}
