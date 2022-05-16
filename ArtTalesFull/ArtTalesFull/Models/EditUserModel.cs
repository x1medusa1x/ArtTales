using ArtTalesFull.Entities;
using System.ComponentModel.DataAnnotations;

namespace ArtTalesFull.Models
{
    public class EditUserModel : BaseEntity
    {
        public string? UserId { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public bool Disabled { get; set; }
    }
}
