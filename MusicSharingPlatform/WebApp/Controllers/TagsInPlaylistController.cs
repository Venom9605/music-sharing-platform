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

public class TagsInPlaylistController : Controller
{
    private readonly AppDbContext _context;
    private readonly ITagsInPlaylistRepository _tagsInPlaylistRepository;

    public TagsInPlaylistController(AppDbContext context, ITagsInPlaylistRepository tagsInPlaylistRepository)
    {
        _context = context;
        _tagsInPlaylistRepository = tagsInPlaylistRepository;
    }

    // GET: TagsInPlaylist
    public async Task<IActionResult> Index()
    {
        return View(await _tagsInPlaylistRepository.AllAsync(User.GetUserId()));
    }

    // GET: TagsInPlaylist/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tagsInPlaylist = await _context.TagsInPlaylists
            .Include(t => t.Playlist)
            .Include(t => t.Tag)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (tagsInPlaylist == null)
        {
            return NotFound();
        }

        return View(tagsInPlaylist);
    }

    // GET: TagsInPlaylist/Create
    public IActionResult Create()
    {
        ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Name");
        ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Name");
        return View();
    }

    // POST: TagsInPlaylist/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PlaylistId,TagId,Id")] TagsInPlaylist tagsInPlaylist)
    {
        if (ModelState.IsValid)
        {
            tagsInPlaylist.Id = Guid.NewGuid();
            _context.Add(tagsInPlaylist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Name", tagsInPlaylist.PlaylistId);
        ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Name", tagsInPlaylist.TagId);
        return View(tagsInPlaylist);
    }

    // GET: TagsInPlaylist/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tagsInPlaylist = await _context.TagsInPlaylists.FindAsync(id);
        if (tagsInPlaylist == null)
        {
            return NotFound();
        }
        ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Name", tagsInPlaylist.PlaylistId);
        ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Name", tagsInPlaylist.TagId);
        return View(tagsInPlaylist);
    }

    // POST: TagsInPlaylist/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("PlaylistId,TagId,Id")] TagsInPlaylist tagsInPlaylist)
    {
        if (id != tagsInPlaylist.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(tagsInPlaylist);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagsInPlaylistExists(tagsInPlaylist.Id))
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
        ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Name", tagsInPlaylist.PlaylistId);
        ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Name", tagsInPlaylist.TagId);
        return View(tagsInPlaylist);
    }

    // GET: TagsInPlaylist/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tagsInPlaylist = await _context.TagsInPlaylists
            .Include(t => t.Playlist)
            .Include(t => t.Tag)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (tagsInPlaylist == null)
        {
            return NotFound();
        }

        return View(tagsInPlaylist);
    }

    // POST: TagsInPlaylist/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var tagsInPlaylist = await _context.TagsInPlaylists.FindAsync(id);
        if (tagsInPlaylist != null)
        {
            _context.TagsInPlaylists.Remove(tagsInPlaylist);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TagsInPlaylistExists(Guid id)
    {
        return _context.TagsInPlaylists.Any(e => e.Id == id);
    }
}