using System.ComponentModel.DataAnnotations;

namespace Aplzz.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        public int UserId { get; set; }
        
        public virtual Post? Post { get; set; }

        public virtual User? User { get; set; } 
    }
}
