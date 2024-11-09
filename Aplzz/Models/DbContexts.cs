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