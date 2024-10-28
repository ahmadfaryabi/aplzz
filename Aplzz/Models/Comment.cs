using System.ComponentModel.DataAnnotations;

namespace Aplzz.Models;

public class Comment
{
    public int Id { get; set; }
    public string? Text { get; set; }
    public DateTime CommentedAt { get; set; }
    public int PostId { get; set; }
    [Required]
    public Post? Post { get; set; }
}
