using System.ComponentModel.DataAnnotations;

namespace Aplzz.Models;

public class Comment
{
    [Key]
    public int CommentId { get; set; }
    
    public string? Text { get; set; }
    [Required]
    public int UserId {get;set;}
    public DateTime CommentedAt { get; set; } = DateTime.Now;
    [Required]
    public int PostId { get; set; }
    public virtual Post? Post { get; set; }
    public virtual User GetUser {get;set;}
}
