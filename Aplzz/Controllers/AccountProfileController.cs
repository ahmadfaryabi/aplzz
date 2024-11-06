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

namespace Aplzz.Controllers
{
<<<<<<< HEAD
<<<<<<< HEAD
  
    public class AccountProfileController : Controller
=======
      public class AccountProfileController : Controller
>>>>>>> 4ad0b03 (AccountProfile update)
=======
    public class AccountProfileController : Controller
>>>>>>> 6322eac (ok)
    {
        private readonly AccountDbContext _context;

        public AccountProfileController(AccountDbContext context)
        {
            _context = context;
        }

<<<<<<< HEAD
<<<<<<< HEAD
        // GET: Display list of profiles
        public async Task<IActionResult> Index()
        {
            var profiles = await _context.AccountProfiles.ToListAsync();
            var viewModel = new AccountProfileViewModel
            {
                Profiles = profiles,
                CurrentViewName = "User Profiles"
            };

            return View(viewModel);
        }

            
        // GET: Display details of a profile by ID
        public async Task<IActionResult> Details(int id)
        {
            var profile = await _context.AccountProfiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            var viewModel = new AccountProfileViewModel
            {
                
                AccountId = profile.AccountId,
                Username = profile.Username ?? string.Empty,
                Bio = profile.Bio,
                ProfilePicture = profile.ProfilePicture,
                CreatedAt = profile.CreatedAt,
                UpdatedAt = profile.UpdatedAt
            };

            return View(viewModel);
        }

        // GET: Show form for creating a new profile
        public IActionResult Create()
        {
            return View(new AccountProfileViewModel());
        }

        // POST: Create a new profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var profile = new AccountProfile
                {
                    Username = viewModel.Username,
                    Bio = viewModel.Bio,
                    ProfilePicture = viewModel.ProfilePicture,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _context.AccountProfiles.Add(profile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Show form for editing an existing profile by ID
        public async Task<IActionResult> Update(int id)
        {
            var profile = await _context.AccountProfiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            var viewModel = new AccountProfileViewModel
            {
                AccountId = profile.AccountId,
                Username = profile.Username ?? string.Empty,
                Bio = profile.Bio,
                ProfilePicture = profile.ProfilePicture
            };

            return View(viewModel);
        }

        // POST: Update an existing profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, AccountProfileViewModel viewModel)
        {
            if (id != viewModel.AccountId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var profile = await _context.AccountProfiles.FindAsync(id);
                if (profile == null)
                {
                    return NotFound();
                }

                profile.Username = viewModel.Username;
                profile.Bio = viewModel.Bio;
                profile.ProfilePicture = viewModel.ProfilePicture;
                profile.UpdatedAt = DateTime.Now;

                _context.Entry(profile).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Confirm deletion of a profile by ID
        public async Task<IActionResult> Delete(int id)
        {
            var profile = await _context.AccountProfiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            var viewModel = new AccountProfileViewModel
            {
                AccountId = profile.AccountId,
                Username = profile.Username ?? string.Empty,
                Bio = profile.Bio,
                ProfilePicture = profile.ProfilePicture
            };

            return View(viewModel);
        }

        // POST: Delete an existing profile
=======
        // READ: Display a list of profiles
=======
        // GET: Display list of profiles
>>>>>>> 6322eac (ok)
        public async Task<IActionResult> Index()
        {
            var profiles = await _context.AccountProfiles.ToListAsync();
            var viewModel = new AccountProfileViewModel
            {
                Profiles = profiles,
                CurrentViewName = "User Profiles"
            };

            return View(viewModel);
        }

        // GET: Display details of a profile by ID
        public async Task<IActionResult> Details(int id)
        {
            var profile = await _context.AccountProfiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            var viewModel = new AccountProfileViewModel
            {
                AccountId = profile.AccountId,
                Username = profile.Username,
                Bio = profile.Bio,
                ProfilePicture = profile.ProfilePicture,
                CreatedAt = profile.CreatedAt,
                UpdatedAt = profile.UpdatedAt
            };

            return View(viewModel);
        }

        // GET: Show form for creating a new profile
        public IActionResult Create()
        {
            return View(new AccountProfileViewModel());
        }

        // POST: Create a new profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var profile = new AccountProfile
                {
                    Username = viewModel.Username,
                    Bio = viewModel.Bio,
                    ProfilePicture = viewModel.ProfilePicture,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _context.AccountProfiles.Add(profile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Show form for editing an existing profile by ID
        public async Task<IActionResult> Update(int id)
        {
            var profile = await _context.AccountProfiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            var viewModel = new AccountProfileViewModel
            {
                AccountId = profile.AccountId,
                Username = profile.Username,
                Bio = profile.Bio,
                ProfilePicture = profile.ProfilePicture
            };

            return View(viewModel);
        }

        // POST: Update an existing profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, AccountProfileViewModel viewModel)
        {
            if (id != viewModel.AccountId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var profile = await _context.AccountProfiles.FindAsync(id);
                if (profile == null)
                {
                    return NotFound();
                }

                profile.Username = viewModel.Username;
                profile.Bio = viewModel.Bio;
                profile.ProfilePicture = viewModel.ProfilePicture;
                profile.UpdatedAt = DateTime.Now;

                _context.Entry(profile).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Confirm deletion of a profile by ID
        public async Task<IActionResult> Delete(int id)
        {
            var profile = await _context.AccountProfiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            var viewModel = new AccountProfileViewModel
            {
                AccountId = profile.AccountId,
                Username = profile.Username,
                Bio = profile.Bio,
                ProfilePicture = profile.ProfilePicture
            };

            return View(viewModel);
        }

<<<<<<< HEAD
        // DELETE: Remove a profile from the database
>>>>>>> 4ad0b03 (AccountProfile update)
=======
        // POST: Delete an existing profile
>>>>>>> 6322eac (ok)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profile = await _context.AccountProfiles.FindAsync(id);
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> 6322eac (ok)
            if (profile == null)
            {
                return NotFound();
            }

<<<<<<< HEAD
=======
>>>>>>> 4ad0b03 (AccountProfile update)
=======
>>>>>>> 6322eac (ok)
            _context.AccountProfiles.Remove(profile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
<<<<<<< HEAD
<<<<<<< HEAD
}
=======
}
>>>>>>> 4ad0b03 (AccountProfile update)
=======
}
>>>>>>> 6322eac (ok)
