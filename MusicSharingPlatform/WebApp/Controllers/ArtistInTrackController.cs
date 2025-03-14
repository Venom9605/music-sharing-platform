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
    public class ArtistInTrackController : Controller
    {
        private readonly AppDbContext _context;

        public ArtistInTrackController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ArtistInTrack
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ArtistsInTracks.Include(a => a.ArtistRole).Include(a => a.Track).Include(a => a.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ArtistInTrack/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistInTrack = await _context.ArtistsInTracks
                .Include(a => a.ArtistRole)
                .Include(a => a.Track)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artistInTrack == null)
            {
                return NotFound();
            }

            return View(artistInTrack);
        }

        // GET: ArtistInTrack/Create
        public IActionResult Create()
        {
            ViewData["ArtistRoleId"] = new SelectList(_context.ArtistRoles, "Id", "Name");
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ArtistInTrack/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrackId,UserId,ArtistRoleId,Id")] ArtistInTrack artistInTrack)
        {
            if (ModelState.IsValid)
            {
                artistInTrack.Id = Guid.NewGuid();
                _context.Add(artistInTrack);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistRoleId"] = new SelectList(_context.ArtistRoles, "Id", "Name", artistInTrack.ArtistRoleId);
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title", artistInTrack.TrackId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", artistInTrack.UserId);
            return View(artistInTrack);
        }

        // GET: ArtistInTrack/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistInTrack = await _context.ArtistsInTracks.FindAsync(id);
            if (artistInTrack == null)
            {
                return NotFound();
            }
            ViewData["ArtistRoleId"] = new SelectList(_context.ArtistRoles, "Id", "Name", artistInTrack.ArtistRoleId);
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title", artistInTrack.TrackId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", artistInTrack.UserId);
            return View(artistInTrack);
        }

        // POST: ArtistInTrack/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TrackId,UserId,ArtistRoleId,Id")] ArtistInTrack artistInTrack)
        {
            if (id != artistInTrack.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artistInTrack);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistInTrackExists(artistInTrack.Id))
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
            ViewData["ArtistRoleId"] = new SelectList(_context.ArtistRoles, "Id", "Name", artistInTrack.ArtistRoleId);
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title", artistInTrack.TrackId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", artistInTrack.UserId);
            return View(artistInTrack);
        }

        // GET: ArtistInTrack/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistInTrack = await _context.ArtistsInTracks
                .Include(a => a.ArtistRole)
                .Include(a => a.Track)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artistInTrack == null)
            {
                return NotFound();
            }

            return View(artistInTrack);
        }

        // POST: ArtistInTrack/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var artistInTrack = await _context.ArtistsInTracks.FindAsync(id);
            if (artistInTrack != null)
            {
                _context.ArtistsInTracks.Remove(artistInTrack);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistInTrackExists(Guid id)
        {
            return _context.ArtistsInTracks.Any(e => e.Id == id);
        }
    }
}
