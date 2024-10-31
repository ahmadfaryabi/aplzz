using Microsoft.EntityFrameworkCore;
using Aplzz.Models;

namespace Aplzz.DAL
{
    public static class DBInit
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<PostDbContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Seed Users
            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User { Username = "testuser1", Email = "testuser1@example.com" },
                    new User { Username = "testuser2", Email = "testuser2@example.com" }
                };
                context.Users.AddRange(users);
                context.SaveChanges();
            }

            // Seed Posts
            if (!context.Posts.Any())
            {
                var posts = new List<Post>
                {
                    new Post
                    {
                        Content = "Dette er det første innlegget.",
                        CreatedAt = DateTime.Now,
                        ImageUrl = "/images/chickenleg.jpg"
                    },
                    new Post
                    {
                        Content = "Dette er det andre innlegget.",
                        CreatedAt = DateTime.Now,
                        ImageUrl = "/images/pizza.jpg"
                    }
                };
                context.Posts.AddRange(posts);
                context.SaveChanges();
            }

            // Seed Comments
            if (!context.Comments.Any())
            {
                var comments = new List<Comment>
                {
                    new Comment { Text = "Flott innlegg!", CommentedAt = DateTime.Now, PostId = 1 },
                    new Comment { Text = "Veldig informativt.", CommentedAt = DateTime.Now, PostId = 2 }
                };
                context.Comments.AddRange(comments);
                context.SaveChanges();
            }

            // Seed Likes
            if (!context.Likes.Any())
            {
                var likes = new List<Like>
                {
                    new Like { PostId = 1, UserId = 1 },
                    new Like { PostId = 2, UserId = 2 }
                };
                context.Likes.AddRange(likes);
                context.SaveChanges();
            }
        }
    }
}