<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
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
=======
using Aplzz.Models;

namespace Aplzz.DAL 
=======

namespace Aplzz.Models 
>>>>>>> 3748811 (ok)
>>>>>>> 36b1ce8 (ok)
{
    public class DbContexts : DbContext 
    {
        public DbContexts(DbContextOptions<DbContexts> options) : base(options)
        {
<<<<<<< HEAD
            //Database.EnsureCreated();
        }
<<<<<<< HEAD
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
=======
        public DbSet<User>git Users { get; set; } 
>>>>>>> ab8fc79 (ook)
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

<<<<<<< HEAD
>>>>>>> 86d362f (login system endring)
=======
=======
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; } 
        // needed tables for soocial media managements
        public DbSet<Post> Posts { get; set; } 
        public DbSet<Comment> Comments { get; set; } 
    }
}
>>>>>>> 3748811 (ok)
<<<<<<< HEAD
>>>>>>> 36b1ce8 (ok)
=======
=======
// using Microsoft.EntityFrameworkCore;

// namespace Aplzz.Models 
// {
//     public class DbContexts : DbContext 
//     {
//         public DbContexts(DbContextOptions<DbContexts> options) : base(options)
//         {
//             Database.EnsureCreated();
//         }
//         public DbSet<User> Users { get; set; } 
//         // needed tables for soocial media managements
//         public DbSet<Post> Posts { get; set; } 
//         public DbSet<Comment> Comments { get; set; } 
//     }
// }
>>>>>>> c4e2647 (fikset sql lite feil. :))
>>>>>>> ab71774 (fikset sql lite feil. :))
