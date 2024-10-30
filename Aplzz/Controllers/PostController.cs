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
        private readonly PostDbContext _context; // Legg til en privat felt for konteksten
        private readonly ILogger<PostController>_logger;

        // Injiser PostDbContext via konstruktøren
        public PostController(PostDbContext context,ILogger<PostController>logger)
        {
            _context = context;
            _logger = logger;
        }

        // Handling to display the list of posts
        public IActionResult Index()
        {
           
            var posts = _context.Posts
                .Include(p => p.Comments) // Inkluder kommentarer
                .ToList(); // Hent innleggene som en liste
            if(posts.Count == 0) {
               _logger.LogError("[PostController] Post List not found while executing _context.Posts");
               
            }
            
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
                    try {
                        // Lagre bildet på serveren
                       var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageFile.FileName);
                       using (var stream = new FileStream(filePath, FileMode.Create))
                      {
                        await imageFile.CopyToAsync(stream);
                       }
                      post.ImageUrl = $"/images/{imageFile.FileName}"; // Sett filbanen i modellen
                      
                    }
                    catch(Exception e)
                    {
                        _logger.LogError("[PostController] image upload failed for image{@imageFile},error message :{e}",imageFile,e.Message);
                        
                    }
                    
                   
                }

                post.CreatedAt = DateTime.Now; // Sett CreatedAt til nåværende dato og tid
                try{
                  _context.Posts.Add(post);
                   await _context.SaveChangesAsync();
                   return RedirectToAction(nameof(Index));
                } catch(Exception e)
                    {
                        _logger.LogError("[PostController] post creation  failed for post {@post},error message :{e}",post,e.Message);
                        
                    }
                
            }
            _logger.LogWarning("[PostController] Post creation failed{@post}",post);
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

                try{
                 _context.Comments.Add(comment);
                  await _context.SaveChangesAsync();
                } 
                catch{
                    _logger.LogError("[PostController] Failed to add comment for post {PostId}", postId);
                     return BadRequest("Comment creation failed.");
                    
                    }
          }
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var post = _context.Posts.Find(id); 
            if (post == null)
            {
              _logger.LogError("[PostController] Post not found to update the PostId{PostId:0000}",id);
              return BadRequest("Post not found for the PostId");
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
                    try{
                        // Lagre bildet på serveren
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    post.ImageUrl = $"/images/{imageFile.FileName}"; // Sett filbanen i modellen 
                    } catch(Exception e){
                        _logger.LogError("[PostController] image upload failed for image{@imageFile},error message :{e}",imageFile,e.Message);
                        

                }
                   
                }

                post.CreatedAt = DateTime.Now;
                try{
                _context.Posts.Update(post); 
                await _context.SaveChangesAsync(); 
                return RedirectToAction(nameof(Index)); 
                 }
                  catch (Exception e)
                  {
                    _logger.LogError("[PostController] image upload failed for image{@imageFile},error message :{e}",imageFile,e.Message);
                  }
                
            }
            _logger.LogError("");
            return View(post); 
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
            {
                _logger.LogError("[PostController] Post not found for the PostId {PostId:0000}",id);
                return BadRequest("Post not found for the PostId");
            }
            return View(post); 
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id) 
        {
            try{
                var post = _context.Posts.Find(id); 
            if (post == null)
            {
              _logger.LogError("[PostController] post not found for the PostId {PostId:0000}",id); 
              return BadRequest("post not found for the PostId");     
            }
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();  
            } catch(Exception e){
                _logger.LogError(e, "[PostController] Error deleting post with ID {PostId}", id);

            }
           
            return RedirectToAction(nameof(Index)); 
        }
    }
}  