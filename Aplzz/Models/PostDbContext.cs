using Microsoft.EntityFrameworkCore;

namespace Aplzz.Models 
{
    public class PostDbContext : DbContext 
    {
        public PostDbContext(DbContextOptions<PostDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Post> Posts { get; set; } 
        public DbSet<Comment> Comments { get; set; } 
        public DbSet<Like> Likes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

