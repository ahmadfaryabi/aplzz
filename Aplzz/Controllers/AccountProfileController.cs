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
      public class AccountProfileController : Controller
    {
        private readonly AccountDbContext _context;

        public AccountProfileController(AccountDbContext context)
        {
            _context = context;
        }

        // READ: Display a list of profiles
        public async Task<IActionResult> Index()
        {
            var profiles = await _context.AccountProfiles.ToListAsync();
            return View(profiles);
        }

        // CREATE: Display form for new profile
        public IActionResult Create()
        {
            return View();
        }

        // CREATE: Add a new profile to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountProfile profile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profile);
        }

        // READ: Display a specific profile
        public async Task<IActionResult> Details(int id)
        {
            var profile = await _context.AccountProfiles.FindAsync(id);
            if (profile == null) return NotFound();
            return View(profile);
        }

        // UPDATE: Display form for editing a profile
        public async Task<IActionResult> Edit(int id)
        {
            var profile = await _context.AccountProfiles.FindAsync(id);
            if (profile == null) return NotFound();
            return View(profile);
        }

        // UPDATE: Edit a specific profile in the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AccountProfile profile)
        {
            if (id != profile.AccountId) return NotFound();
            
            if (ModelState.IsValid)
            {
                profile.UpdatedAt = DateTime.Now;
                _context.Update(profile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profile);
        }

        // DELETE: Confirm profile deletion
        public async Task<IActionResult> Delete(int id)
        {
            var profile = await _context.AccountProfiles.FindAsync(id);
            if (profile == null) return NotFound();
            return View(profile);
        }

        // DELETE: Remove a profile from the database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profile = await _context.AccountProfiles.FindAsync(id);
            _context.AccountProfiles.Remove(profile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
