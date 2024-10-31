using Microsoft.EntityFrameworkCore;
using Aplzz.Models;

namespace Aplzz.DAL 
{
    public class DbContexts : DbContext 
    {
        public DbContexts(DbContextOptions<DbContexts> options) : base(options)
        {
<<<<<<< HEAD
            Database.EnsureCreated();
=======
            //Database.EnsureCreated();
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
        }
        public DbSet<User> Users { get; set; } 
        // needed tables for soocial media managements
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

