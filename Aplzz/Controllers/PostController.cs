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
<<<<<<< HEAD
using Aplzz.DAL;
=======
>>>>>>> 7ae0213 (La til test user for å teste like funksjonen)

namespace Aplzz.Controllers
{
    
    public class PostController : Controller
    {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        private readonly IPostRepository _postRepository; // Legg til en privat felt for konteksten
        private readonly ILogger<PostController> _logger; // Legg til logger

        // Injiser PostDbContext via konstruktøren
        public PostController(IPostRepository postRepository, ILogger<PostController> logger)
        {
            _postRepository = postRepository;
            _logger = logger; // Initialiser logger
=======
=======
>>>>>>> 86d362f (login system endring)
=======
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
=======
>>>>>>> fd4c2ae (fikset på stiling vedr. login/registrering)
=======
>>>>>>> 8109aa9 (accountprofile)
        private readonly PostDbContext _context; // Legg til en privat felt for konteksten
<<<<<<< HEAD
<<<<<<< HEAD
        private readonly ILogger<PostController>_logger;

        // Injiser PostDbContext via konstruktøren
        public PostController(PostDbContext context,ILogger<PostController>logger)
=======
        private readonly DbContexts _context; // Legg til en privat felt for konteksten

        // Injiser PostDbContext via konstruktøren
        public PostController(DbContexts context)
>>>>>>> f4ab8f9 (login system endring)
=======
=======
>>>>>>> 1c5c828 (fikset på stiling vedr. login/registrering)
        private readonly ILogger<PostController> _logger;
        private readonly IPostRepository _postRepository;

        // Injiser PostDbContext via konstruktøren
        public PostController(IPostRepository postRepository, ILogger<PostController> logger)
>>>>>>> 7cf30d2 (fiksing)
        {
<<<<<<< HEAD
            _context = context;
            _logger = logger;
=======
        private readonly ILogger<PostController> _logger; // Legg til logger
=======
        private readonly IPostRepository _postRepository;
        private readonly ILogger<PostController> _logger;
>>>>>>> d6afb7a (Lagt til DAL, Fikset Like og Kommentar funksjon)

        public PostController(IPostRepository postRepository, ILogger<PostController> logger)
        {
<<<<<<< HEAD
            _context = context;
            _logger = logger; // Initialiser logger
>>>>>>> ff3fccc (La til test user for å teste like funksjonen)
=======
            _postRepository = postRepository;
            _logger = logger;
>>>>>>> d6afb7a (Lagt til DAL, Fikset Like og Kommentar funksjon)
=======
            _postRepository = postRepository;
            _logger = logger;
>>>>>>> 7071121 (ok)
=======
        private readonly DbContexts _context; // Legg til en privat felt for konteksten
        private readonly ILogger<PostController> _logger; // Legg til logger

        // Injiser PostDbContext via konstruktøren
        public PostController(DbContexts context, ILogger<PostController> logger)
        {
            _context = context;
            _logger = logger; // Initialiser logger
>>>>>>> 5d12ca7 (accountprofile)
        }

        // Handling to display the list of posts
        public IActionResult Index()
        {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
           
            var posts = _context.Posts
                .Include(p => p.Comments) // Inkluder kommentarer
                .ToList(); // Hent innleggene som en liste
            if(posts.Count == 0) {
               _logger.LogError("[PostController] Post List not found while executing _context.Posts");
               
            }
            
            var viewModel = new PostViewModel(posts, "Aplzz Feed");
            return View(viewModel);
<<<<<<< HEAD
>>>>>>> c954901 (Errohandling og logging)
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

=======
=======
=======
>>>>>>> 7071121 (ok)
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
<<<<<<< HEAD
>>>>>>> d6afb7a (Lagt til DAL, Fikset Like og Kommentar funksjon)
=======
>>>>>>> 7071121 (ok)
=======
            var posts = _context.Posts
                .Include(p => p.Comments) // Inkluder kommentarer
                .ToList(); // Hent innleggene som en liste

            var viewModel = new PostViewModel(posts, "Aplzz Feed");
            return View(viewModel);
>>>>>>> 5d12ca7 (accountprofile)
        }

>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
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
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
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
=======
=======
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
=======
>>>>>>> 6322eac (ok)
=======
>>>>>>> 8109aa9 (accountprofile)
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
                
<<<<<<< HEAD
>>>>>>> c954901 (Errohandling og logging)
=======
=======
                    try
=======
                    // Lagre bildet på serveren
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
>>>>>>> 5d12ca7 (accountprofile)
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    post.ImageUrl = $"/images/{imageFile.FileName}"; // Sett filbanen i modellen
                }

<<<<<<< HEAD
=======
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

>>>>>>> 7071121 (ok)
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
<<<<<<< HEAD
>>>>>>> d6afb7a (Lagt til DAL, Fikset Like og Kommentar funksjon)
<<<<<<< HEAD
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
=======
=======
>>>>>>> 7071121 (ok)
<<<<<<< HEAD
>>>>>>> 6322eac (ok)
            }
            _logger.LogWarning("[PostController] Post creation failed{@post}",post);
            return View(post);
        }
<<<<<<< HEAD
            
        
 
=======
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
=======
=======
                post.CreatedAt = DateTime.Now; // Sett CreatedAt til nåværende dato og tid
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
>>>>>>> 5d12ca7 (accountprofile)
            }
            _logger.LogWarning("[PostController] Post creation failed{@post}",post);
            return View(post);
        }    
>>>>>>> 8109aa9 (accountprofile)

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

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
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
=======
=======
>>>>>>> 7ae0213 (La til test user for å teste like funksjonen)
=======
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
=======
>>>>>>> 6322eac (ok)
=======
>>>>>>> 8109aa9 (accountprofile)
                try{
                 _context.Comments.Add(comment);
                  await _context.SaveChangesAsync();
                } 
                catch{
                    _logger.LogError("[PostController] Failed to add comment for post {PostId}", postId);
                     return BadRequest("Comment creation failed.");
                    
                    }
          }
            
=======
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Kommentar lagt til: {CommentId} for postId: {PostId}", comment.CommentId, postId);
            }
            else
            {
                _logger.LogWarning("Kommentartekst er tom for postId: {PostId}", postId);
            }

>>>>>>> ff3fccc (La til test user for å teste like funksjonen)
            return RedirectToAction(nameof(Index));
<<<<<<< HEAD
>>>>>>> c954901 (Errohandling og logging)
=======
=======
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
>>>>>>> 7071121 (ok)
>>>>>>> 6322eac (ok)
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var post = await _postRepository.GetPostById(id);
            if (post == null)
            {
<<<<<<< HEAD
                _logger.LogError("[PostController] Post not found when updating the postId{postId:0000}", id);
                return NotFound("Post not found for the postId");
=======
              _logger.LogError("[PostController] Post not found to update the PostId{PostId:0000}",id);
              return BadRequest("Post not found for the PostId");
<<<<<<< HEAD
>>>>>>> c954901 (Errohandling og logging)
=======
=======
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Kommentar lagt til: {CommentId} for postId: {PostId}", comment.CommentId, postId);
            }
            else
            {
                _logger.LogWarning("Kommentartekst er tom for postId: {PostId}", postId);
>>>>>>> 5d12ca7 (accountprofile)
>>>>>>> 8109aa9 (accountprofile)
            }

<<<<<<< HEAD
        [HttpPost]
        public async Task<IActionResult> Update(Post post, IFormFile? imageFile)
        {
<<<<<<< HEAD
            var existingPost = await _postRepository.GetPostById(post.PostId); // Hent eksisterende innlegg
            if (existingPost == null)
            {
<<<<<<< HEAD
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
=======
=======
            var existingPost = await _context.Posts.FindAsync(post.PostId); // Hent eksisterende innlegg
            if (existingPost == null)
            {
<<<<<<< HEAD
>>>>>>> 7ae0213 (La til test user for å teste like funksjonen)
                if (imageFile != null && imageFile.Length > 0)
                {
<<<<<<< HEAD
                    try{
                        // Lagre bildet på serveren
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
=======
                    try
>>>>>>> 7071121 (ok)
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
<<<<<<< HEAD
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
<<<<<<< HEAD
>>>>>>> c954901 (Errohandling og logging)
=======
=======
                return NotFound(); // Returner 404 hvis innlegget ikke finnes
            }

            // Oppdater feltene i eksisterende innlegg
            existingPost.Content = post.Content; // Oppdater innhold

            if (imageFile != null && imageFile.Length > 0)
            {
                // Lagre bildet på serveren
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
=======
                var result = await _postRepository.AddComment(comment);
                if (!result)
>>>>>>> d6afb7a (Lagt til DAL, Fikset Like og Kommentar funksjon)
                {
                    return BadRequest(new { error = "Kunne ikke legge til kommentar" });
                }
                return Json(new { 
                    text = comment.Text,
                    commentedAt = comment.CommentedAt.ToString("dd.MM.yyyy HH:mm")
                });
            }
<<<<<<< HEAD

            existingPost.CreatedAt = DateTime.Now; // Oppdater CreatedAt
            _context.Posts.Update(existingPost); 
            await _context.SaveChangesAsync(); 
            return RedirectToAction(nameof(Index)); 
>>>>>>> ff3fccc (La til test user for å teste like funksjonen)
<<<<<<< HEAD
>>>>>>> 7ae0213 (La til test user for å teste like funksjonen)
=======
=======
            return BadRequest(new { error = "Kommentartekst kan ikke være tom" });
>>>>>>> d6afb7a (Lagt til DAL, Fikset Like og Kommentar funksjon)
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
        }
            return View(post);
        }
            

        [HttpGet]
<<<<<<< HEAD
        public async Task<IActionResult> Delete(int id)
=======
        public async Task<IActionResult> Update(int id)
<<<<<<< HEAD
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
=======
=======
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
>>>>>>> 7071121 (ok)
>>>>>>> 6322eac (ok)
        {
            var post = await _postRepository.GetPostById(id);
            if (post == null)
            {
<<<<<<< HEAD
                _logger.LogError("[PostController] Post not found for PostId{PostId:0000}", id);
                return NotFound("Post not found for the PostId");
=======
                _logger.LogError("[PostController] Post not found for the PostId {PostId:0000}",id);
                return BadRequest("Post not found for the PostId");
>>>>>>> c954901 (Errohandling og logging)
            }
            return View(post);
        }

        [HttpPost]
<<<<<<< HEAD
<<<<<<< HEAD
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
<<<<<<< HEAD
<<<<<<< HEAD
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
                    var like = new Like { PostId = postId, UserId = userId };
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



        
    


=======
=======
>>>>>>> 7ae0213 (La til test user for å teste like funksjonen)
=======
=======
>>>>>>> 6322eac (ok)
        public async Task<IActionResult> Update(Post post, IFormFile? imageFile)
        {
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
            try{
                var post = _context.Posts.Find(id); 
=======
            var post = await _context.Posts.FindAsync(id); 
>>>>>>> ff3fccc (La til test user for å teste like funksjonen)
=======
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
>>>>>>> d6afb7a (Lagt til DAL, Fikset Like og Kommentar funksjon)
            if (post == null)
=======
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _postRepository.Delete(id);
            if (!result)
>>>>>>> 7071121 (ok)
            {
              _logger.LogError("[PostController] post not found for the PostId {PostId:0000}",id); 
              return BadRequest("post not found for the PostId");     
            }
<<<<<<< HEAD
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();  
            } catch(Exception e){
                _logger.LogError(e, "[PostController] Error deleting post with ID {PostId}", id);

            }
           
            return RedirectToAction(nameof(Index)); 
        }

        [HttpPost]
public async Task<IActionResult> LikePost(int postId)
{
    int userId = 1; // Hardkode userId for testbrukeren "testuser"
    _logger.LogInformation("Bruker {UserId} liker postId: {PostId}", userId, postId);

    var postExists = await _context.Posts.AnyAsync(p => p.PostId == postId);
    if (!postExists)
    {
        _logger.LogWarning("Post med postId: {PostId} eksisterer ikke.", postId);
        return NotFound();
    }
<<<<<<< HEAD
}  
>>>>>>> c954901 (Errohandling og logging)
=======
<<<<<<< HEAD
}  
=======

    var existingLike = await _context.Likes
        .FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);

    if (existingLike != null)
    {
        // Hvis det allerede finnes en like fra denne brukeren, fjern den
        _context.Likes.Remove(existingLike);
        _logger.LogInformation("Fjerner like for postId: {PostId} av bruker {UserId}", postId, userId);
    }
    else
    {
        // Hvis ingen like finnes, legg til en ny
        var like = new Like
=======
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
>>>>>>> d6afb7a (Lagt til DAL, Fikset Like og Kommentar funksjon)
        {
            var result = await _postRepository.Delete(id);
            if (!result)
            {
                return NotFound();
            }
=======
>>>>>>> 5d12ca7 (accountprofile)
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
<<<<<<< HEAD
<<<<<<< HEAD
}
>>>>>>> ff3fccc (La til test user for å teste like funksjonen)
<<<<<<< HEAD
>>>>>>> 7ae0213 (La til test user for å teste like funksjonen)
=======
=======
}
>>>>>>> 7071121 (ok)
<<<<<<< HEAD
>>>>>>> 6322eac (ok)
=======
=======
}

>>>>>>> 5d12ca7 (accountprofile)
>>>>>>> 8109aa9 (accountprofile)
