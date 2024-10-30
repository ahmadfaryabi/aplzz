namespace Aplzz.Models;

public class Post
{
<<<<<<< HEAD
    public Post()
    {
        Likes = new List<Like>();
    }

    public int PostId { get; set; }
    public required string Content { get; set; }
=======
    public int PostId { get; set; }
    public string? Content { get; set; }
>>>>>>> 7ae0213 (La til test user for å teste like funksjonen)

    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual List<Comment>? Comments { get; set; }
<<<<<<< HEAD
    public virtual List<Like> Likes { get; set; }
=======
    public virtual List<Like>? Likes { get; set; }
>>>>>>> 7ae0213 (La til test user for å teste like funksjonen)
}
