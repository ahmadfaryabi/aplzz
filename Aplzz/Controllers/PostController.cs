using System;
using System.Collections.Generic;
using System.Linq; // Legg til for LINQ
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Aplzz.Models;
using Aplzz.ViewModels;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Aplzz.Controllers
{
    public class PostController : Controller
    {
        private readonly DbContexts _context; // Legg til en privat felt for konteksten

        // Injiser PostDbContext via konstruktøren
        public PostController(DbContexts context)
        {
            _context = context;
        }

        // Handling to display the list of posts
        public IActionResult Index()
        {
            var posts = _context.Posts
                .Include(p => p.Comments) // Inkluder kommentarer
                .ToList(); // Hent innleggene som en liste

            var viewModel = new PostViewModel(posts, "Aplzz Feed");
            return View(viewModel);
        }


        [HttpGet]
        public IActionResult Create()
        {
        return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Post post, IFormFile imageFile)
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

                post.CreatedAt = DateTime.Now;
                _context.Posts.Update(post); 
                await _context.SaveChangesAsync(); 
                return RedirectToAction(nameof(Index)); 
            }
            return View(post); 
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
            var post = _context.Posts.Find(id); 
            if (post == null)
            {
                return NotFound();
            }
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync(); 
            return RedirectToAction(nameof(Index)); 
        }
    }
}
