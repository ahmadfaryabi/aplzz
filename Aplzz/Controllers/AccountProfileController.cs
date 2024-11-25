using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Aplzz.Models;
using Aplzz.ViewModels;
using Microsoft.VisualBasic;
using Aplzz.DAL;

namespace Aplzz.Controllers
{
  
    public class AccountProfileController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<AccountProfileController> _logger;


        public AccountProfileController(IAccountRepository accountRepository, ILogger<AccountProfileController> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        // GET: Display the profile
        [Route("AccountProfile/{username}")]
        public async Task<IActionResult> Index(string username)
        {
            if(username == "") {
                return View("NotFound", username);
            }
            var user = await _accountRepository.GetUserInfo(username);
            if(user == null) {
                return View("NotFound", user);
            }
            return View(user);
        }
    }
}
            
//         // GET: Display details of a profile by ID
//         public async Task<IActionResult> Details(int id)
//         {
//             var profile = await _context.AccountProfiles.FindAsync(id);
//             if (profile == null)
//             {
//                 return NotFound();
//             }

//             var viewModel = new AccountProfile
//             {
                
//                 AccountId = profile.AccountId,
//                 Username = profile.Username ?? string.Empty,
//                 Bio = profile.Bio,
//                 ProfilePicture = profile.ProfilePicture,
//                 CreatedAt = profile.CreatedAt,
//                 UpdatedAt = profile.UpdatedAt
//             };

//             return View(viewModel);
//         }

//         // GET: Show form for creating a new profile
//         public IActionResult Create()
//         {
//             return View(new AccountProfile());
//         }

//         // POST: Create a new profile
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create(AccountProfile views)
//         {
//             if (ModelState.IsValid)
//             {
//                 var profile = new AccountProfile
//                 {
//                     Username = views.Username,
//                     Bio = views.Bio,
//                     ProfilePicture = views.ProfilePicture,
//                     CreatedAt = DateTime.Now,
//                     UpdatedAt = DateTime.Now
//                 };

//                 _context.AccountProfiles.Add(profile);
//                 await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }

//             return View(views);
//         }

//         // GET: Show form for editing an existing profile by ID
//         public async Task<IActionResult> Update(int id)
//         {
//             var profile = await _context.AccountProfiles.FindAsync(id);
//             if (profile == null)
//             {
//                 return NotFound();
//             }
//             };

//             return View(viewModel);
//         }

//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(int id)
//         {
//             var profile = await _context.AccountProfiles.FindAsync(id);
//             if (profile == null)
//             {
//                 return NotFound();
//             }

//             _context.AccountProfiles.Remove(profile);
//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
