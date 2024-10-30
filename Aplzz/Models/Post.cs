namespace Aplzz.Models;

public class Post
{
    public int PostId { get; set; }
    public string? Content { get; set; }

    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual List<Comment>? Comments { get; set; }
    public virtual List<Like>? Likes { get; set; }
}
