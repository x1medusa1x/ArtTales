namespace ArtTalesFull.Entities
{
    public class Artwork : BaseEntity
    {
        public string? Description { get; set; }

        public string? Name { get; set; }

        public int Likes { get; set; }

        public string? Headline { get; set; }

        public string? UserId { get; set; }

        public int PreviewImage { get; set; }

    }
}
