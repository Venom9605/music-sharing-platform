using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using Domain;

namespace WebApp.Controllers
{
    public class UserSavedTrackController : Controller
    {
        private readonly AppDbContext _context;

        public UserSavedTrackController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserSavedTrack
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserSavedTracks.Include(u => u.Track);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserSavedTrack/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSavedTracks = await _context.UserSavedTracks
                .Include(u => u.Track)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userSavedTracks == null)
            {
                return NotFound();
            }

            return View(userSavedTracks);
        }

        // GET: UserSavedTrack/Create
        public IActionResult Create()
        {
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "CoverPath");
            return View();
        }

        // POST: UserSavedTrack/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrackId,ArtistId,SavedAt,Id")] UserSavedTracks userSavedTracks)
        {
            if (ModelState.IsValid)
            {
                userSavedTracks.Id = Guid.NewGuid();
                _context.Add(userSavedTracks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "CoverPath", userSavedTracks.TrackId);
            return View(userSavedTracks);
        }

        // GET: UserSavedTrack/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSavedTracks = await _context.UserSavedTracks.FindAsync(id);
            if (userSavedTracks == null)
            {
                return NotFound();
            }
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "CoverPath", userSavedTracks.TrackId);
            return View(userSavedTracks);
        }

        // POST: UserSavedTrack/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TrackId,ArtistId,SavedAt,Id")] UserSavedTracks userSavedTracks)
        {
            if (id != userSavedTracks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userSavedTracks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserSavedTracksExists(userSavedTracks.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "CoverPath", userSavedTracks.TrackId);
            return View(userSavedTracks);
        }

        // GET: UserSavedTrack/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSavedTracks = await _context.UserSavedTracks
                .Include(u => u.Track)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userSavedTracks == null)
            {
                return NotFound();
            }

            return View(userSavedTracks);
        }

        // POST: UserSavedTrack/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userSavedTracks = await _context.UserSavedTracks.FindAsync(id);
            if (userSavedTracks != null)
            {
                _context.UserSavedTracks.Remove(userSavedTracks);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserSavedTracksExists(Guid id)
        {
            return _context.UserSavedTracks.Any(e => e.Id == id);
        }
    }
}
