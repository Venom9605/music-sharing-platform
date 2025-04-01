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

public class UserLinkController : Controller
{
    private readonly AppDbContext _context;

    public UserLinkController(AppDbContext context)
    {
        _context = context;
    }

    // GET: UserLink
    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.UserLinks.Include(u => u.LinkType).Include(u => u.User);
        return View(await appDbContext.ToListAsync());
    }

    // GET: UserLink/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userLink = await _context.UserLinks
            .Include(u => u.LinkType)
            .Include(u => u.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (userLink == null)
        {
            return NotFound();
        }

        return View(userLink);
    }

    // GET: UserLink/Create
    public IActionResult Create()
    {
        ViewData["LinkTypeId"] = new SelectList(_context.LinkTypes, "Id", "Name");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        return View();
    }

    // POST: UserLink/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("UserId,LinkTypeId,Url,Id")] UserLink userLink)
    {
        if (ModelState.IsValid)
        {
            userLink.Id = Guid.NewGuid();
            _context.Add(userLink);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["LinkTypeId"] = new SelectList(_context.LinkTypes, "Id", "Name", userLink.LinkTypeId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userLink.UserId);
        return View(userLink);
    }

    // GET: UserLink/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userLink = await _context.UserLinks.FindAsync(id);
        if (userLink == null)
        {
            return NotFound();
        }
        ViewData["LinkTypeId"] = new SelectList(_context.LinkTypes, "Id", "Name", userLink.LinkTypeId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userLink.UserId);
        return View(userLink);
    }

    // POST: UserLink/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("UserId,LinkTypeId,Url,Id")] UserLink userLink)
    {
        if (id != userLink.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(userLink);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLinkExists(userLink.Id))
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
        ViewData["LinkTypeId"] = new SelectList(_context.LinkTypes, "Id", "Name", userLink.LinkTypeId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userLink.UserId);
        return View(userLink);
    }

    // GET: UserLink/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userLink = await _context.UserLinks
            .Include(u => u.LinkType)
            .Include(u => u.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (userLink == null)
        {
            return NotFound();
        }

        return View(userLink);
    }

    // POST: UserLink/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var userLink = await _context.UserLinks.FindAsync(id);
        if (userLink != null)
        {
            _context.UserLinks.Remove(userLink);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool UserLinkExists(Guid id)
    {
        return _context.UserLinks.Any(e => e.Id == id);
    }
}