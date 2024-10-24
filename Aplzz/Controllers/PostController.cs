using System;
using System.Collections.Generic;
using System.Linq; // Legg til for LINQ
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Aplzz.Models;
using Aplzz.ViewModels;
using System.Globalization;

namespace Aplzz.Controllers
{
    public class PostController : Controller
    {
        private readonly PostDbContext _context; // Legg til en privat felt for konteksten

        // Injiser PostDbContext via konstruktøren
        public PostController(PostDbContext context)
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
    }
}
