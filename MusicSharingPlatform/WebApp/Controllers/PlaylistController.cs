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

public class PlaylistController : Controller
{
    private readonly AppDbContext _context;
    private readonly IPlaylistRepository _playlistRepository;

    public PlaylistController(AppDbContext context, IPlaylistRepository playlistRepository)
    {
        _context = context;
        _playlistRepository = playlistRepository;
    }

    // GET: Playlist
    public async Task<IActionResult> Index()
    {
        return View(await _playlistRepository.AllAsync(User.GetUserId()));
    }

    // GET: Playlist/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var playlist = await _context.Playlists
            .Include(p => p.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (playlist == null)
        {
            return NotFound();
        }

        return View(playlist);
    }

    // GET: Playlist/Create
    public IActionResult Create()
    {
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        return View();
    }

    // POST: Playlist/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("UserId,Name,Description,CoverUrl,IsPublic,CreatedAt,Id")] Playlist playlist)
    {
        if (ModelState.IsValid)
        {
            playlist.Id = Guid.NewGuid();
            _context.Add(playlist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", playlist.UserId);
        return View(playlist);
    }

    // GET: Playlist/Edit/5

    // POST: Playlist/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("UserId,Name,Description,CoverUrl,IsPublic,CreatedAt,Id")] Playlist playlist)
    {
        if (id != playlist.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(playlist);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaylistExists(playlist.Id))
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
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", playlist.UserId);
        return View(playlist);
    }

    // GET: Playlist/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var playlist = await _context.Playlists
            .Include(p => p.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (playlist == null)
        {
            return NotFound();
        }

        return View(playlist);
    }

    // POST: Playlist/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var playlist = await _context.Playlists.FindAsync(id);
        if (playlist != null)
        {
            _context.Playlists.Remove(playlist);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PlaylistExists(Guid id)
    {
        return _context.Playlists.Any(e => e.Id == id);
    }
}