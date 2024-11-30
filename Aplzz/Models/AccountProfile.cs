using System;
using System.ComponentModel.DataAnnotations;

namespace Aplzz.Models
{
    public class AccountProfile
    {
        [Key]
        public int AccountId { get; set; }

        [Required(ErrorMessage ="Username required")]
        [RegularExpression(@"^[0-9a-zA-ZæøåÆØÅ.\-]{2,100}$",
           ErrorMessage ="Username must only contain letters or numbers,and be  between 2 to 100 characters.")]
         public string? Username { get; set; }

        [MaxLength(200)]
        public string? Bio { get; set; }

        [MaxLength(250)]
        public string? ProfilePicture { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}