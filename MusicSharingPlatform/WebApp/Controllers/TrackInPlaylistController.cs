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

public class TrackInPlaylistController : Controller
{
    private readonly AppDbContext _context;
    private readonly ITrackInPlaylistRepository _trackInPlaylistRepository;

    public TrackInPlaylistController(AppDbContext context, ITrackInPlaylistRepository trackInPlaylistRepository)
    {
        _context = context;
        _trackInPlaylistRepository = trackInPlaylistRepository;
    }

    // GET: TrackInPlaylist
    public async Task<IActionResult> Index()
    {
        return View(await _trackInPlaylistRepository.AllAsync(User.GetUserId()));
    }

    // GET: TrackInPlaylist/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var trackInPlaylist = await _context.TracksInPlaylists
            .Include(t => t.Playlist)
            .Include(t => t.Track)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (trackInPlaylist == null)
        {
            return NotFound();
        }

        return View(trackInPlaylist);
    }

    // GET: TrackInPlaylist/Create
    public IActionResult Create()
    {
        ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Name");
        ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title");
        return View();
    }

    // POST: TrackInPlaylist/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TrackId,PlaylistId,Id")] TrackInPlaylist trackInPlaylist)
    {
        if (ModelState.IsValid)
        {
            trackInPlaylist.Id = Guid.NewGuid();
            _context.Add(trackInPlaylist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Name", trackInPlaylist.PlaylistId);
        ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title", trackInPlaylist.TrackId);
        return View(trackInPlaylist);
    }

    // GET: TrackInPlaylist/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var trackInPlaylist = await _context.TracksInPlaylists.FindAsync(id);
        if (trackInPlaylist == null)
        {
            return NotFound();
        }
        ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Name", trackInPlaylist.PlaylistId);
        ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title", trackInPlaylist.TrackId);
        return View(trackInPlaylist);
    }

    // POST: TrackInPlaylist/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("TrackId,PlaylistId,Id")] TrackInPlaylist trackInPlaylist)
    {
        if (id != trackInPlaylist.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(trackInPlaylist);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackInPlaylistExists(trackInPlaylist.Id))
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
        ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Name", trackInPlaylist.PlaylistId);
        ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title", trackInPlaylist.TrackId);
        return View(trackInPlaylist);
    }

    // GET: TrackInPlaylist/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var trackInPlaylist = await _context.TracksInPlaylists
            .Include(t => t.Playlist)
            .Include(t => t.Track)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (trackInPlaylist == null)
        {
            return NotFound();
        }

        return View(trackInPlaylist);
    }

    // POST: TrackInPlaylist/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var trackInPlaylist = await _context.TracksInPlaylists.FindAsync(id);
        if (trackInPlaylist != null)
        {
            _context.TracksInPlaylists.Remove(trackInPlaylist);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TrackInPlaylistExists(Guid id)
    {
        return _context.TracksInPlaylists.Any(e => e.Id == id);
    }
}