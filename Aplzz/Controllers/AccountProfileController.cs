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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Aplzz.Controllers
{
  
    public class AccountProfileController : Controller
    {
        private readonly AccountDbContext _accountDbContext;

        public AccountProfileController(AccountDbContext accountDbContext)
        {
            _accountDbContext = accountDbContext;
        }

        // GET: Display list of profiles
        public async Task<IActionResult> Index()
        {
            List<AccountProfile> accountProfiles = _accountDbContext.AccountProfiles.ToList();
            var profiles = await _accountDbContext.AccountProfiles.ToListAsync();
             var accountProfileViewModel = new AccountProfileViewModel
            //var views = new AccountProfile
            {
                Profiles = profiles,
                CurrentViewName = "User Profiles"
            };

            return View(accountProfileViewModel);
        }

            
        // GET: Display details of a profile by ID
        public async Task<IActionResult> Details(int id)
        {
            var profile = await _accountDbContext.AccountProfiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            var AccountViewModel = new AccountProfile
            {
                
                AccountId = profile.AccountId,
                Username = profile.Username ?? string.Empty,
                Bio = profile.Bio,
                ProfilePicture = profile.ProfilePicture,
                CreatedAt = profile.CreatedAt,
                UpdatedAt = profile.UpdatedAt
            };

            return View(AccountViewModel);
        }

        // GET: Show form for creating a new profile
        public IActionResult Create()
        {
            return View(new AccountProfile());
        }

        // POST: Create a new profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountProfile views)
        {
            if (ModelState.IsValid)
            {

                        // Ensure the AccountProfileAccountId exists in the database
             //   var existingProfile = await _accountDbContext.AccountProfiles.FindAsync(views.AccountProfileAccountId);
             //   if (existingProfile == null)
              //  {
                    // Handle the case when the referenced AccountProfileAccountId doesn't exist
               //     ModelState.AddModelError("AccountProfileAccountId", "The specified AccountProfileAccountId does not exist.");
             //       return View(views); // Return to the form with an error message
             //   }

                var profile = new AccountProfile
                {
                    Username = views.Username,
                    Bio = views.Bio,
                    ProfilePicture = views.ProfilePicture,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    AccountProfileAccountId = views.AccountProfileAccountId
                };

                 // Check if the username already exists
                    bool isUsernameTaken = await _accountDbContext.AccountProfiles
                        .AnyAsync(p => p.Username == profile.Username);
                    if (isUsernameTaken)
                    {
                        TempData["ErrorUserName"] = "Username already exists, please choose another one.";
                        return View(views); // Return with error message
                    }
                
                 _accountDbContext.AccountProfiles.Add(profile);
                await _accountDbContext.SaveChangesAsync();

                // Success message for user feedback
        TempData["SuccessMsg"] = "Account profile created successfully! You can now log in.";

                return RedirectToAction(nameof(Index));
            }

        
            return View(views);
        }

      
        // GET: Show form for editing an existing profile by ID
        public async Task<IActionResult> Update(int id)
        {
            var profile = await _accountDbContext.AccountProfiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            var viewModel = new AccountProfile
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
        public async Task<IActionResult> Update(int id, AccountProfile viewModel)
        {
            if (id != viewModel.AccountId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var profile = await _accountDbContext.AccountProfiles.FindAsync(id);
                if (profile == null)
                {
                    return NotFound();
                }

                profile.Username = viewModel.Username;
                profile.Bio = viewModel.Bio;
                profile.ProfilePicture = viewModel.ProfilePicture;
                profile.UpdatedAt = DateTime.Now;

                _accountDbContext.Entry(profile).State = EntityState.Modified;
                await _accountDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Confirm deletion of a profile by ID
        public async Task<IActionResult> Delete(int id)
        {
            var profile = await _accountDbContext.AccountProfiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            var viewModel = new AccountProfile
            {
                AccountId = profile.AccountId,
                Username = profile.Username ?? string.Empty,
                Bio = profile.Bio,
                ProfilePicture = profile.ProfilePicture
            };

             return View(viewModel);
        }

        // POST: Delete an existing profile
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profile = await _accountDbContext.AccountProfiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            _accountDbContext.AccountProfiles.Remove(profile);
            await _accountDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}