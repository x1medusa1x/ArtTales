using Microsoft.AspNetCore.Identity;

namespace ArtTalesFull.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? AppUsername { get; set; }

        public string? ProfilePic { get; set; }

        public bool Disabled { get; set; }

    }
}
