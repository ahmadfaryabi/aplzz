using Aplzz.Models;
using System;
using System.ComponentModel.DataAnnotations;


   namespace Aplzz.ViewModels
{
    public class AccountProfileViewModel
    {
        public int AccountId { get; set; } // Needed for Update/Delete actions

        [Required]
        [MaxLength(100)]
        [Display(Name = "Username")]
        public string Username { get; set; } // Username of the profile

        [MaxLength(500)]
        [Display(Name = "Bio")]
        public string Bio { get; set; } // Bio of the profile

        [Display(Name = "Profile Picture URL")]
        [DataType(DataType.ImageUrl)]
        public string ProfilePicture { get; set; } // URL or path to profile picture

        [Display(Name = "Date Created")]
        public DateTime CreatedAt { get; set; } // Date profile was created

        [Display(Name = "Last Updated")]
        public DateTime UpdatedAt { get; set; } // Date profile was last updated

        // New properties added for Index view
        public IEnumerable<AccountProfile> Profiles { get; set; } // List of profiles for Index
        public string CurrentViewName { get; set; }               // Additional metadata
    }
}