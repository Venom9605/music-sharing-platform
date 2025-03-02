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
    public class MoodsInPlaylistController : Controller
    {
        private readonly AppDbContext _context;

        public MoodsInPlaylistController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MoodsInPlaylist
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.MoodsInPlaylists.Include(m => m.Mood).Include(m => m.Playlist);
            return View(await appDbContext.ToListAsync());
        }

        // GET: MoodsInPlaylist/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moodsInPlaylist = await _context.MoodsInPlaylists
                .Include(m => m.Mood)
                .Include(m => m.Playlist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moodsInPlaylist == null)
            {
                return NotFound();
            }

            return View(moodsInPlaylist);
        }

        // GET: MoodsInPlaylist/Create
        public IActionResult Create()
        {
            ViewData["MoodId"] = new SelectList(_context.Moods, "Id", "Name");
            ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Description");
            return View();
        }

        // POST: MoodsInPlaylist/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoodId,PlaylistId,Id")] MoodsInPlaylist moodsInPlaylist)
        {
            if (ModelState.IsValid)
            {
                moodsInPlaylist.Id = Guid.NewGuid();
                _context.Add(moodsInPlaylist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MoodId"] = new SelectList(_context.Moods, "Id", "Name", moodsInPlaylist.MoodId);
            ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Description", moodsInPlaylist.PlaylistId);
            return View(moodsInPlaylist);
        }

        // GET: MoodsInPlaylist/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moodsInPlaylist = await _context.MoodsInPlaylists.FindAsync(id);
            if (moodsInPlaylist == null)
            {
                return NotFound();
            }
            ViewData["MoodId"] = new SelectList(_context.Moods, "Id", "Name", moodsInPlaylist.MoodId);
            ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Description", moodsInPlaylist.PlaylistId);
            return View(moodsInPlaylist);
        }

        // POST: MoodsInPlaylist/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MoodId,PlaylistId,Id")] MoodsInPlaylist moodsInPlaylist)
        {
            if (id != moodsInPlaylist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moodsInPlaylist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoodsInPlaylistExists(moodsInPlaylist.Id))
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
            ViewData["MoodId"] = new SelectList(_context.Moods, "Id", "Name", moodsInPlaylist.MoodId);
            ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Description", moodsInPlaylist.PlaylistId);
            return View(moodsInPlaylist);
        }

        // GET: MoodsInPlaylist/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moodsInPlaylist = await _context.MoodsInPlaylists
                .Include(m => m.Mood)
                .Include(m => m.Playlist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moodsInPlaylist == null)
            {
                return NotFound();
            }

            return View(moodsInPlaylist);
        }

        // POST: MoodsInPlaylist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var moodsInPlaylist = await _context.MoodsInPlaylists.FindAsync(id);
            if (moodsInPlaylist != null)
            {
                _context.MoodsInPlaylists.Remove(moodsInPlaylist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoodsInPlaylistExists(Guid id)
        {
            return _context.MoodsInPlaylists.Any(e => e.Id == id);
        }
    }
}
