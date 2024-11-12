using System;
using System.Collections.Generic;
using System.Linq; // Legg til for LINQ
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Aplzz.Models;
using Aplzz.DAL;
using Aplzz.ViewModels;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Aplzz.Controllers
{
    
    public class PostController : Controller
    {

        private readonly DbContexts _context; // Legg til en privat felt for konteksten
        private readonly ILogger<PostController> _logger; // Legg til logger

        // Injiser PostDbContext via konstruktøren
        public PostController(DbContexts context, ILogger<PostController> logger)

        private readonly PostDbContext _context; // Legg til en privat felt for konteksten

        private readonly ILogger<PostController> _logger;
        private readonly IPostRepository _postRepository;

        // Injiser PostDbContext via konstruktøren
        public PostController(IPostRepository postRepository, ILogger<PostController> logger)

        {
            _context = context;
            _logger = logger; // Initialiser logger
        }

        // Handling to display the list of posts
      //  public async Task<IActionResult> Index()
       // {
       //     var posts = await _context.Posts
        //        .Where(p => p.PostId > 0) // or another condition involving PostId
        //        .ToListAsync();

        //    var viewModel = new PostViewModel(posts, "Aplzz Feed");
        //    return View(viewModel);
       // }

        [HttpGet]
        public IActionResult Create()
        {
        return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Post post, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Lagre bildet på serveren
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    post.ImageUrl = $"/images/{imageFile.FileName}"; // Sett filbanen i modellen
                }

                post.CreatedAt = DateTime.Now; // Sett CreatedAt til nåværende dato og tid
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }    

        [HttpPost]
        public async Task<IActionResult> AddComment(int postId, string commentText)
        {
            _logger.LogInformation("Legger til kommentar: {CommentText} til postId: {PostId}", commentText, postId);

            if (!string.IsNullOrEmpty(commentText))
            {
                var comment = new Comment
                {
                    Text = commentText,
                    CommentedAt = DateTime.Now,
                    PostId = postId
                };

                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Kommentar lagt til: {CommentId} for postId: {PostId}", comment.CommentId, postId);
            }
            else
            {
                _logger.LogWarning("Kommentartekst er tom for postId: {PostId}", postId);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var post = _context.Posts.Find(id); 
            if (post == null)
            {
                return NotFound();
            }
            return View(post); 
        }

        [HttpPost]
        public async Task<IActionResult> Update(Post post, IFormFile imageFile) 
        {
            var existingPost = await _context.Posts.FindAsync(post.PostId); // Hent eksisterende innlegg
            if (existingPost == null)
            {
                return NotFound(); // Returner 404 hvis innlegget ikke finnes
            }

            // Oppdater feltene i eksisterende innlegg
            existingPost.Content = post.Content; // Oppdater innhold

            if (imageFile != null && imageFile.Length > 0)
            {
                // Lagre bildet på serveren
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                existingPost.ImageUrl = $"/images/{imageFile.FileName}"; // Sett filbanen i modellen
            }

            existingPost.CreatedAt = DateTime.Now; // Oppdater CreatedAt
            _context.Posts.Update(existingPost); 
            await _context.SaveChangesAsync(); 
            return RedirectToAction(nameof(Index)); 
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post); 
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id) 
        {
            var post = await _context.Posts.FindAsync(id); 
            if (post == null)
            {
                return NotFound();
            }
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync(); 
            return RedirectToAction(nameof(Index)); 
        }

        [HttpPost]
public async Task<IActionResult> LikePost(int postId)
{
    int userId = 1; // Hardkode userId for testbrukeren "testuser"
    _logger.LogInformation("Bruker {IdUser} liker postId: {PostId}", userId, postId);

    var postExists = await _context.Posts.AnyAsync(p => p.PostId == postId);
    if (!postExists)
    {
        _logger.LogWarning("Post med postId: {PostId} eksisterer ikke.", postId);
        return NotFound();
    }

    var existingLike = await _context.Likes
        .FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);

    if (existingLike != null)
    {
        // Hvis det allerede finnes en like fra denne brukeren, fjern den
        _context.Likes.Remove(existingLike);
        _logger.LogInformation("Fjerner like for postId: {PostId} av bruker {IdUser}", postId, userId);
    }
    else
    {
        // Hvis ingen like finnes, legg til en ny
        var like = new Like
        {
            PostId = postId,
            UserId = userId
        };
        _context.Likes.Add(like);
        _logger.LogInformation("Legger til like for postId: {PostId} av bruker {UserId}", postId, userId);
    }

    await _context.SaveChangesAsync();

    // Returner oppdatert like-telling
    var likeCount = await _context.Likes.CountAsync(l => l.PostId == postId);
    return Json(new { likesCount = likeCount });
}

        [HttpPost]
        public async Task<IActionResult> CreateTestUser()
        {
            var testUser = new User
            {
                Username = "testuser",
                Email = "testuser@example.com"
            };

            _context.Users.Add(testUser);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

