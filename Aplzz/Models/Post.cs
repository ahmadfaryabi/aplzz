namespace Aplzz.Models;

public class Post
{
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
    public Post()
    {
        Likes = new List<Like>();
    }

<<<<<<< HEAD
    public int PostId { get; set; }
    public required string Content { get; set; }
=======
    public int PostId { get; set; }
    public string? Content { get; set; }
>>>>>>> 7ae0213 (La til test user for å teste like funksjonen)
=======
    public int PostId { get; set; }
    public required string Content { get; set; }
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)

    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual List<Comment>? Comments { get; set; }
<<<<<<< HEAD
<<<<<<< HEAD
    public virtual List<Like> Likes { get; set; }
=======
    public virtual List<Like>? Likes { get; set; }
>>>>>>> 7ae0213 (La til test user for å teste like funksjonen)
=======
    public virtual List<Like> Likes { get; set; }
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
}
