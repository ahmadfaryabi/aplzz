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
using Microsoft.Extensions.Logging;
using Aplzz.DAL;

namespace Aplzz.Controllers
{
    
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository; // Legg til en privat felt for konteksten
        private readonly ILogger<PostController> _logger; // Legg til logger

        // Injiser PostDbContext via konstruktøren
        public PostController(IPostRepository postRepository, ILogger<PostController> logger)
        {
            _postRepository = postRepository;
            _logger = logger; // Initialiser logger
        }

       // Handling to display the list of posts
       public async Task<IActionResult> Index()
       {
           var posts = await _postRepository.GetAll();
           if(posts == null) {
              _logger.LogError("[PostController] Post List not found while executing _postRepository.GetAll()");
               return NotFound("Post not found");
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
        public async Task<IActionResult> Create(Post post, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    try
                    {
                        // Lagre bildet på serveren
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }
                        post.ImageUrl = $"/images/{imageFile.FileName}"; // Sett filbanen i modellen
                }
                    catch (Exception e)
                    {
                        _logger.LogError("[PostController] Image upload failed: {e}", e.Message);
                    }
                }

                post.CreatedAt = DateTime.Now; // Sett CreatedAt til nåværende dato og tid
                try
                {
                    await _postRepository.Create(post);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    _logger.LogError("[PostController] Failed to create post: {e}", e.Message);
                }
                _logger.LogWarning("[PostController] post creation has failed {@post}",post);
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

                try 
                {
                    await _postRepository.AddComment(comment);
                    _logger.LogInformation("Kommentar lagt til: {CommentId} for postId: {PostId}", comment.CommentId, postId);
                    
                    return Json(new { 
                        text = comment.Text,
                        commentedAt = comment.CommentedAt.ToString("dd.MM.yyyy HH:mm")
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError("[PostController] Failed to add comment: {e}", e.Message);
                    return BadRequest(new { error = "Kunne ikke legge til kommentar" });
                }
            }
            
            _logger.LogWarning("Kommentartekst er tom for postId: {PostId}", postId);
            return BadRequest(new { error = "Kommentartekst kan ikke være tom" });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var post = await _postRepository.GetPostById(id);
            if (post == null)
            {
                _logger.LogError("[PostController] Post not found when updating the postId{postId:0000}", id);
                return NotFound("Post not found for the postId");
            }
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Post post, IFormFile imageFile) 
        {
            var existingPost = await _postRepository.GetPostById(post.PostId); // Hent eksisterende innlegg
            if (existingPost == null)
            {
                _logger.LogError("[PostController] Post List not found while executing _postRepository.GetAll()");
                return NotFound(); // Returner 404 hvis innlegget ikke finnes
            }

            // Oppdater feltene i eksisterende innlegg
            existingPost.Content = post.Content; // Oppdater innhold

            if (imageFile != null && imageFile.Length > 0)
            {
                try{

                // Lagre bildet på serveren
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                existingPost.ImageUrl = $"/images/{imageFile.FileName}"; // Sett filbanen i modellen

                } catch (Exception e)
            {
                _logger.LogError("[PostController] Image upload failed: {e}", e.Message);
            }

                
            }

            existingPost.CreatedAt = DateTime.Now; // Oppdater CreatedAt 
            try{
                await _postRepository.Update(existingPost);; 
                return RedirectToAction(nameof(Index)); 
            }  catch (Exception e)
        {
            _logger.LogError("[PostController] Failed to update post: {e}", e.Message);
        }
            return View(post);
        }
            

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _postRepository.GetPostById(id);
            if (post == null)
            {
                _logger.LogError("[PostController] Post not found for PostId{PostId:0000}", id);
                return NotFound("Post not found for the PostId");
            }
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _postRepository.GetPostById(id);
            if (post == null)
            {
                _logger.LogError("[PostController] Post not found for PostId{PostId:0000}", id);
                return NotFound("Post not found");
            }

            bool result = await _postRepository.Delete(id);
            if (!result)
            {
                _logger.LogError("[PostController] Post deletion failed for the PostId{PostId:0000}", id);
                return BadRequest("Post deletion failed");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> LikePost(int postId)
        {
            try
            {
                int userId = 1; // Hardkodet bruker-ID
                var isLiked = await _postRepository.HasUserLikedPost(postId, userId);

                if (isLiked)
                {
                    await _postRepository.RemoveLike(postId, userId);
                }
                else
                {
                    var like = new Like { PostId = postId, UserId = userId };  // Endret fra IdUser til UserId
                    await _postRepository.AddLike(like);
                }

                var newLikeCount = await _postRepository.GetLikeCount(postId);
                return Json(new { likesCount = newLikeCount, isLiked = !isLiked });
            }
            catch (Exception e)
            {
                _logger.LogError("[PostController] Failed to process like: {e}", e.Message);
                return BadRequest("Failed to process like");
            }
        }
    }
}



        
    


