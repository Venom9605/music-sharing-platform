using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using Domain;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers;

[Authorize]

public class TrackLinkController : Controller
{
    private readonly AppDbContext _context;

    public TrackLinkController(AppDbContext context)
    {
        _context = context;
    }

    // GET: TrackLink
    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.TrackLinks.Include(t => t.LinkType).Include(t => t.Track);
        return View(await appDbContext.ToListAsync());
    }

    // GET: TrackLink/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var trackLink = await _context.TrackLinks
            .Include(t => t.LinkType)
            .Include(t => t.Track)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (trackLink == null)
        {
            return NotFound();
        }

        return View(trackLink);
    }

    // GET: TrackLink/Create
    public IActionResult Create()
    {
        ViewData["LinkTypeId"] = new SelectList(_context.LinkTypes, "Id", "Name");
        ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title");
        return View();
    }

    // POST: TrackLink/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TrackId,LinkTypeId,Url,Id")] TrackLink trackLink)
    {
        if (ModelState.IsValid)
        {
            trackLink.Id = Guid.NewGuid();
            _context.Add(trackLink);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["LinkTypeId"] = new SelectList(_context.LinkTypes, "Id", "Name", trackLink.LinkTypeId);
        ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title", trackLink.TrackId);
        return View(trackLink);
    }

    // GET: TrackLink/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var trackLink = await _context.TrackLinks.FindAsync(id);
        if (trackLink == null)
        {
            return NotFound();
        }
        ViewData["LinkTypeId"] = new SelectList(_context.LinkTypes, "Id", "Name", trackLink.LinkTypeId);
        ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title", trackLink.TrackId);
        return View(trackLink);
    }

    // POST: TrackLink/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("TrackId,LinkTypeId,Url,Id")] TrackLink trackLink)
    {
        if (id != trackLink.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(trackLink);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackLinkExists(trackLink.Id))
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
        ViewData["LinkTypeId"] = new SelectList(_context.LinkTypes, "Id", "Name", trackLink.LinkTypeId);
        ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title", trackLink.TrackId);
        return View(trackLink);
    }

    // GET: TrackLink/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var trackLink = await _context.TrackLinks
            .Include(t => t.LinkType)
            .Include(t => t.Track)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (trackLink == null)
        {
            return NotFound();
        }

        return View(trackLink);
    }

    // POST: TrackLink/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var trackLink = await _context.TrackLinks.FindAsync(id);
        if (trackLink != null)
        {
            _context.TrackLinks.Remove(trackLink);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TrackLinkExists(Guid id)
    {
        return _context.TrackLinks.Any(e => e.Id == id);
    }
}