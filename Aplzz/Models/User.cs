using System.ComponentModel.DataAnnotations;

namespace Aplzz.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Brukernavn er påkrevd.")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "E-post er påkrevd.")]
        public required string Email { get; set; }

        public virtual List<Like> Likes { get; set; } = new List<Like>();
    }
}
