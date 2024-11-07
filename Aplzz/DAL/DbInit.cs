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
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> b70eb87 (Endret DbInit)
            
            if (!context.Database.CanConnect())
            {
                context.Database.EnsureCreated();
            }
<<<<<<< HEAD

            // Seed Users først
=======
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Seed Users
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
=======

            // Seed Users først
>>>>>>> b70eb87 (Endret DbInit)
            if (!context.Users.Any())
            {
                var users = new List<User>
                {
<<<<<<< HEAD
<<<<<<< HEAD
                    new User {IdUser = 1, Firstname = "Ahmad", Aftername="Faryabi",Username="ahmad", Password="1234", Phone="12345678", Email="email@email.com",
                    ProfilePicture="images/profile.jpeg"}
=======
                    new User { Username = "testuser1", Email = "testuser1@example.com" },
                    new User { Username = "testuser2", Email = "testuser2@example.com" }
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
=======
                    new User {IdUser = 1, Firstname = "Ahmad", Aftername="Faryabi",Username="ahmad", Password="1234", Phone="12345678", Email="email@email.com",
                    ProfilePicture="images/profile.jpeg"}
>>>>>>> 2b5b12d (fiksing)
                };
                context.Users.AddRange(users);
                context.SaveChanges();
            }

<<<<<<< HEAD
<<<<<<< HEAD
            // Seed Posts og lagre PostId-ene
            List<int> postIds = new List<int>();
=======
            // Seed Posts
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
=======
            // Seed Posts og lagre PostId-ene
            List<int> postIds = new List<int>();
>>>>>>> b70eb87 (Endret DbInit)
            if (!context.Posts.Any())
            {
                var posts = new List<Post>
                {
                    new Post
                    {
                        Content = "Dette er det første innlegget.",
                        CreatedAt = DateTime.Now,
<<<<<<< HEAD
<<<<<<< HEAD
                        ImageUrl = "/images/pexels.jpg"
=======
                        ImageUrl = "/images/chickenleg.jpg"
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
=======
                        ImageUrl = "/images/pexels.jpg"
>>>>>>> b70eb87 (Endret DbInit)
                    },
                    new Post
                    {
                        Content = "Dette er det andre innlegget.",
                        CreatedAt = DateTime.Now,
<<<<<<< HEAD
<<<<<<< HEAD
                        ImageUrl = "/images/scott.jpg"
=======
                        ImageUrl = "/images/pizza.jpg"
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
=======
                        ImageUrl = "/images/scott.jpg"
>>>>>>> b70eb87 (Endret DbInit)
                    }
                };
                context.Posts.AddRange(posts);
                context.SaveChanges();
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> b70eb87 (Endret DbInit)
                postIds = posts.Select(p => p.PostId).ToList();
            }
            else
            {
                postIds = context.Posts.Select(p => p.PostId).ToList();
<<<<<<< HEAD
            }

            // Seed Comments kun hvis vi har gyldige innlegg
            if (!context.Comments.Any() && postIds.Any())
            {
                var comments = new List<Comment>
                {
                    new Comment { Text = "Flott innlegg!", CommentedAt = DateTime.Now, PostId = postIds[0] },
                    new Comment { Text = "Veldig informativt.", CommentedAt = DateTime.Now, PostId = postIds.Last() }
=======
=======
>>>>>>> b70eb87 (Endret DbInit)
            }

            // Seed Comments kun hvis vi har gyldige innlegg
            if (!context.Comments.Any() && postIds.Any())
            {
                var comments = new List<Comment>
                {
<<<<<<< HEAD
                    new Comment { Text = "Flott innlegg!", CommentedAt = DateTime.Now, PostId = 1 },
                    new Comment { Text = "Veldig informativt.", CommentedAt = DateTime.Now, PostId = 2 }
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
=======
                    new Comment { Text = "Flott innlegg!", CommentedAt = DateTime.Now, PostId = postIds[0] },
                    new Comment { Text = "Veldig informativt.", CommentedAt = DateTime.Now, PostId = postIds.Last() }
>>>>>>> b70eb87 (Endret DbInit)
                };
                context.Comments.AddRange(comments);
                context.SaveChanges();
            }

<<<<<<< HEAD
<<<<<<< HEAD
            // Seed Likes kun hvis vi har gyldige innlegg og brukere
            if (!context.Likes.Any() && postIds.Any() && context.Users.Any())
            {
                var userIds = context.Users.Select(u => u.IdUser).ToList();
                var likes = new List<Like>
                {
                    new Like { PostId = postIds[0], UserId = userIds[0] },
                    new Like { PostId = postIds.Last(), UserId = userIds.Last() }
=======
            // Seed Likes
            if (!context.Likes.Any())
=======
            // Seed Likes kun hvis vi har gyldige innlegg og brukere
            if (!context.Likes.Any() && postIds.Any() && context.Users.Any())
>>>>>>> b70eb87 (Endret DbInit)
            {
                var userIds = context.Users.Select(u => u.UserId).ToList();
                var likes = new List<Like>
                {
<<<<<<< HEAD
                    new Like { PostId = 1, UserId = 1 },
                    new Like { PostId = 2, UserId = 2 }
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
=======
                    new Like { PostId = postIds[0], UserId = userIds[0] },
                    new Like { PostId = postIds.Last(), UserId = userIds.Last() }
>>>>>>> b70eb87 (Endret DbInit)
                };
                context.Likes.AddRange(likes);
                context.SaveChanges();
            }
        }
    }
}