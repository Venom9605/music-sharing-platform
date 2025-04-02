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
using Domain;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers;

[Authorize]

public class TrackController : Controller
{
    private readonly AppDbContext _context;
    private readonly ITrackRepository _trackRepository;

    public TrackController(AppDbContext context, ITrackRepository trackRepository)
    {
        _context = context;
        _trackRepository = trackRepository;
    }

    // GET: Track
    public async Task<IActionResult> Index()
    {
        var res = await _trackRepository.AllAsync();
        return View(res);
    }

    // GET: Track/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var track = await _context.Tracks
            .FirstOrDefaultAsync(m => m.Id == id);
        if (track == null)
        {
            return NotFound();
        }

        return View(track);
    }

    // GET: Track/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Track/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,FilePath,CoverPath,Uploaded,Duration,TimesPlayed,TimesSaved,Id")] Track track)
    {
        if (ModelState.IsValid)
        {
            track.Id = Guid.NewGuid();
            track.Uploaded = DateTime.UtcNow;
            _context.Add(track);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(track);
    }

    // GET: Track/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var track = await _context.Tracks.FindAsync(id);
        if (track == null)
        {
            return NotFound();
        }
        return View(track);
    }

    // POST: Track/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Title,FilePath,CoverPath,Uploaded,Duration,TimesPlayed,TimesSaved,Id")] Track track)
    {
        if (id != track.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(track);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackExists(track.Id))
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
        return View(track);
    }

    // GET: Track/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var track = await _context.Tracks
            .FirstOrDefaultAsync(m => m.Id == id);
        if (track == null)
        {
            return NotFound();
        }

        return View(track);
    }

    // POST: Track/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var track = await _context.Tracks.FindAsync(id);
        if (track != null)
        {
            _context.Tracks.Remove(track);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TrackExists(Guid id)
    {
        return _context.Tracks.Any(e => e.Id == id);
    }
}