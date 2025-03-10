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
    public class TagsInTrackController : Controller
    {
        private readonly AppDbContext _context;

        public TagsInTrackController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TagsInTrack
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.TagsInTracks.Include(t => t.Tag).Include(t => t.Track);
            return View(await appDbContext.ToListAsync());
        }

        // GET: TagsInTrack/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagsInTrack = await _context.TagsInTracks
                .Include(t => t.Tag)
                .Include(t => t.Track)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tagsInTrack == null)
            {
                return NotFound();
            }

            return View(tagsInTrack);
        }

        // GET: TagsInTrack/Create
        public IActionResult Create()
        {
            ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Name");
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title");
            return View();
        }

        // POST: TagsInTrack/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrackId,TagId,Id")] TagsInTrack tagsInTrack)
        {
            if (ModelState.IsValid)
            {
                tagsInTrack.Id = Guid.NewGuid();
                _context.Add(tagsInTrack);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Name", tagsInTrack.TagId);
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title", tagsInTrack.TrackId);
            return View(tagsInTrack);
        }

        // GET: TagsInTrack/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagsInTrack = await _context.TagsInTracks.FindAsync(id);
            if (tagsInTrack == null)
            {
                return NotFound();
            }
            ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Name", tagsInTrack.TagId);
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title", tagsInTrack.TrackId);
            return View(tagsInTrack);
        }

        // POST: TagsInTrack/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TrackId,TagId,Id")] TagsInTrack tagsInTrack)
        {
            if (id != tagsInTrack.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tagsInTrack);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagsInTrackExists(tagsInTrack.Id))
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
            ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Name", tagsInTrack.TagId);
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title", tagsInTrack.TrackId);
            return View(tagsInTrack);
        }

        // GET: TagsInTrack/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagsInTrack = await _context.TagsInTracks
                .Include(t => t.Tag)
                .Include(t => t.Track)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tagsInTrack == null)
            {
                return NotFound();
            }

            return View(tagsInTrack);
        }

        // POST: TagsInTrack/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tagsInTrack = await _context.TagsInTracks.FindAsync(id);
            if (tagsInTrack != null)
            {
                _context.TagsInTracks.Remove(tagsInTrack);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TagsInTrackExists(Guid id)
        {
            return _context.TagsInTracks.Any(e => e.Id == id);
        }
    }
}
