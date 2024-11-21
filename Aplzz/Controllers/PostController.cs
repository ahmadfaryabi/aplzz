using Microsoft.AspNetCore.Mvc;
using Aplzz.DAL;
using Aplzz.Models;
using Aplzz.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Aplzz.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository; // Legg til en privat felt for konteksten
        private readonly ILogger<PostController> _logger;

        // Injiser PostDbContext via konstruktøren
        public PostController(IPostRepository postRepository, ILogger<PostController> logger)
        {
            _postRepository = postRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var posts = await _postRepository.GetAll();
                var viewModel = new PostViewModel(posts, "Aplzz Feed");
                return View(viewModel);
            }
            catch (Exception e)
            {
                _logger.LogError("[PostController] Failed to fetch posts: {e}", e.Message);
                return BadRequest("Failed to fetch posts");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            if(HttpContext.Session.GetString("username") == null) {
                // logg inn først for å entre siden
                return RedirectToAction("Index", "Login");
            }
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
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }
                        post.ImageUrl = $"/images/{imageFile.FileName}";
                    }
                    catch (Exception e)
                    {
                        _logger.LogError("[PostController] Image upload failed: {e}", e.Message);
                    }
                }

                post.CreatedAt = DateTime.Now;
                try
                {
                    await _postRepository.Create(post);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    _logger.LogError("[PostController] Failed to create post: {e}", e.Message);
                }
            }
            if(HttpContext.Session.GetString("username") == null) {
                // logg inn først for å entre siden
                return RedirectToAction("Index", "Login");
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

                var result = await _postRepository.AddComment(comment);
                if (!result)
                {
                    return BadRequest(new { error = "Kunne ikke legge til kommentar" });
                }
                return Json(new { 
                    text = comment.Text,
                    commentedAt = comment.CommentedAt.ToString("dd.MM.yyyy HH:mm")
                });
            }
            return BadRequest(new { error = "Kommentartekst kan ikke være tom" });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var post = await _postRepository.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            if(HttpContext.Session.GetString("username") == null) {
                // logg inn først for å entre siden
                return RedirectToAction("Index", "Login");
            }
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Post post, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    try
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }
                        post.ImageUrl = $"/images/{imageFile.FileName}";
                    }
                    catch (Exception e)
                    {
                        _logger.LogError("[PostController] Image upload failed: {e}", e.Message);
                    }
                }
                    post.CreatedAt = DateTime.Now;
                try
                {
                    await _postRepository.Update(post);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    _logger.LogError("[PostController] Failed to update post: {e}", e.Message);
                }
            }
            return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _postRepository.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            if(HttpContext.Session.GetString("username") == null) {
                // logg inn først for å entre siden
                return RedirectToAction("Index", "Login");
            }
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _postRepository.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> LikePost(int postId)
        {
            if(HttpContext.Session.GetInt32("id") == null) {
                // logg inn først for å entre siden
                return RedirectToAction("Index", "Login");
            }
            try
            {
                int userId = (int)HttpContext.Session.GetInt32("id"); // Hardkodet bruker-ID
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