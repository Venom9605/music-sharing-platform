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
using Microsoft.Identity.Client;

namespace WebApp.Controllers;

[Authorize]

public class UserSavedTrackController : Controller
{
    private readonly AppDbContext _context;
    private readonly IUserSavedTracksRepository _userSavedTracksRepository;

    public UserSavedTrackController(AppDbContext context, IUserSavedTracksRepository userSavedTracksRepository)
    {
        _context = context;
        _userSavedTracksRepository = userSavedTracksRepository;
    }

    // GET: UserSavedTrack
    public async Task<IActionResult> Index()
    {
        return View(await _userSavedTracksRepository.AllAsync(User.GetUserId()));
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
            .Include(u => u.User)
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
        ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        return View();
    }

    // POST: UserSavedTrack/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TrackId,UserId,SavedAt,Id")] UserSavedTracks userSavedTracks)
    {
        if (ModelState.IsValid)
        {
            userSavedTracks.Id = Guid.NewGuid();
            userSavedTracks.SavedAt = DateTime.UtcNow;
            _context.Add(userSavedTracks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title", userSavedTracks.TrackId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userSavedTracks.UserId);
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
        ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title", userSavedTracks.TrackId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userSavedTracks.UserId);
        return View(userSavedTracks);
    }

    // POST: UserSavedTrack/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("TrackId,UserId,SavedAt,Id")] UserSavedTracks userSavedTracks)
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
        ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title", userSavedTracks.TrackId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userSavedTracks.UserId);
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
            .Include(u => u.User)
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