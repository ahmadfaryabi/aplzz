
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Aplzz.Models;

public class Post
{
    public Post()
    {
        Likes = new List<Like>();
    }

    [Key]
    public int PostId { get; set; }

    [Required(ErrorMessage ="Content is required.")]
    [StringLength(1000,ErrorMessage ="Content max is 1000 characters")]
    public string Content { get; set; }
    [Url(ErrorMessage ="Please provide a valid url")]
    public string? ImageUrl { get; set; }

    [Required(ErrorMessage ="UserId is required.")]
    public int UserId { get; set; }
    public virtual User? GetUser {get;set;}
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public virtual List<Comment>? Comments { get; set; }
    public virtual List<Like> Likes { get; set; }

    public static implicit operator Post(Task<IEnumerable<Post>> v)
    {
        throw new NotImplementedException();
    }
}