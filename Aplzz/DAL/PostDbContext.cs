using Microsoft.EntityFrameworkCore;
using Aplzz.Models;

namespace Aplzz.DAL 
{
<<<<<<< HEAD
<<<<<<< HEAD
    public class DbContexts : DbContext 
    {
        public DbContexts(DbContextOptions<DbContexts> options) : base(options)
        {
<<<<<<< HEAD
<<<<<<< HEAD
            Database.EnsureCreated();
=======
            //Database.EnsureCreated();
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
=======
            Database.EnsureCreated();
>>>>>>> ab71774 (fikset sql lite feil. :))
        }
        public DbSet<User> Users { get; set; } 
        // needed tables for soocial media managements
=======
=======
>>>>>>> d0be505 (fikset på sidene)
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}

