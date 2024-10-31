using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
<<<<<<< HEAD


namespace Aplzz.Models 
=======
using Aplzz.Models;

namespace Aplzz.DAL 
>>>>>>> 86d362f (login system endring)
=======
using Aplzz.Models;

namespace Aplzz.DAL 
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
{
    public class DbContexts : DbContext 
    {
        public DbContexts(DbContextOptions<DbContexts> options) : base(options)
        {
            //Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; } 
<<<<<<< HEAD
        public DbSet<Like> Likes { get; set; } 
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
=======
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

>>>>>>> 86d362f (login system endring)
