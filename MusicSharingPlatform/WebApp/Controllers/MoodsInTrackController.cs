using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.DAL.Interfaces;
using Base.Helpers;
using App.DAL.DTO;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers;

[Authorize]

public class MoodsInTrackController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMoodsInTrackRepository _moodsInTrackRepository;

    public MoodsInTrackController(AppDbContext context, IMoodsInTrackRepository moodsInTrackRepository)
    {
        _context = context;
        _moodsInTrackRepository = moodsInTrackRepository;
    }

    // GET: MoodsInTrack
    public async Task<IActionResult> Index()
    {
        return View(await _moodsInTrackRepository.AllAsync(User.GetUserId()));
    }

    // GET: MoodsInTrack/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var moodsInTrack = await _context.MoodsInTracks
            .Include(m => m.Mood)
            .Include(m => m.Track)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (moodsInTrack == null)
        {
            return NotFound();
        }

        return View(moodsInTrack);
    }

    // GET: MoodsInTrack/Create
    public IActionResult Create()
    {
        ViewData["MoodId"] = new SelectList(_context.Moods, "Id", "Name");
        ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title");
        return View();
    }

    // POST: MoodsInTrack/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MoodId,TrackId,Id")] MoodsInTrack moodsInTrack)
    {
        if (ModelState.IsValid)
        {
            moodsInTrack.Id = Guid.NewGuid();
            _context.Add(moodsInTrack);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["MoodId"] = new SelectList(_context.Moods, "Id", "Name", moodsInTrack.MoodId);
        ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title", moodsInTrack.TrackId);
        return View(moodsInTrack);
    }

    // GET: MoodsInTrack/Edit/5

    // POST: MoodsInTrack/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("MoodId,TrackId,Id")] MoodsInTrack moodsInTrack)
    {
        if (id != moodsInTrack.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(moodsInTrack);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoodsInTrackExists(moodsInTrack.Id))
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
        ViewData["MoodId"] = new SelectList(_context.Moods, "Id", "Name", moodsInTrack.MoodId);
        ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title", moodsInTrack.TrackId);
        return View(moodsInTrack);
    }

    // GET: MoodsInTrack/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var moodsInTrack = await _context.MoodsInTracks
            .Include(m => m.Mood)
            .Include(m => m.Track)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (moodsInTrack == null)
        {
            return NotFound();
        }

        return View(moodsInTrack);
    }

    // POST: MoodsInTrack/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var moodsInTrack = await _context.MoodsInTracks.FindAsync(id);
        if (moodsInTrack != null)
        {
            _context.MoodsInTracks.Remove(moodsInTrack);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MoodsInTrackExists(Guid id)
    {
        return _context.MoodsInTracks.Any(e => e.Id == id);
    }
}