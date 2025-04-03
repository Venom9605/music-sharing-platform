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

public class RatingController : Controller
{
    private readonly AppDbContext _context;
    private readonly IRatingRepository _ratingRepository;

    public RatingController(AppDbContext context, IRatingRepository ratingRepository)
    {
        _context = context;
        _ratingRepository = ratingRepository;
    }

    // GET: Rating
    public async Task<IActionResult> Index()
    {
        return View(await _ratingRepository.AllAsync(User.GetUserId()));
    }

    // GET: Rating/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rating = await _context.Ratings
            .Include(r => r.Track)
            .Include(r => r.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (rating == null)
        {
            return NotFound();
        }

        return View(rating);
    }

    // GET: Rating/Create
    public IActionResult Create()
    {
        ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        return View();
    }

    // POST: Rating/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TrackId,UserId,Score,Comment,Date,Id")] Rating rating)
    {
        if (ModelState.IsValid)
        {
            rating.Id = Guid.NewGuid();
            rating.Date = DateTime.UtcNow;
            _context.Add(rating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title", rating.TrackId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", rating.UserId);
        return View(rating);
    }

    // GET: Rating/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rating = await _context.Ratings.FindAsync(id);
        if (rating == null)
        {
            return NotFound();
        }
        ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title", rating.TrackId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", rating.UserId);
        return View(rating);
    }

    // POST: Rating/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("TrackId,UserId,Score,Comment,Date,Id")] Rating rating)
    {
        if (id != rating.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(rating);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(rating.Id))
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
        ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Title", rating.TrackId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", rating.UserId);
        return View(rating);
    }

    // GET: Rating/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rating = await _context.Ratings
            .Include(r => r.Track)
            .Include(r => r.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (rating == null)
        {
            return NotFound();
        }

        return View(rating);
    }

    // POST: Rating/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var rating = await _context.Ratings.FindAsync(id);
        if (rating != null)
        {
            _context.Ratings.Remove(rating);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RatingExists(Guid id)
    {
        return _context.Ratings.Any(e => e.Id == id);
    }
}