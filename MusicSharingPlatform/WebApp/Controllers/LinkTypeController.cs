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

public class LinkTypeController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILinkTypeRepository _linkTypeRepository;

    public LinkTypeController(AppDbContext context, ILinkTypeRepository linkTypeRepository)
    {
        _context = context;
        _linkTypeRepository = linkTypeRepository;
    }

    // GET: LinkType
    public async Task<IActionResult> Index()
    {
        return View(await _linkTypeRepository.AllAsync(User.GetUserId()));
    }

    // GET: LinkType/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var linkType = await _context.LinkTypes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (linkType == null)
        {
            return NotFound();
        }

        return View(linkType);
    }

    // GET: LinkType/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: LinkType/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Id")] LinkType linkType)
    {
        if (ModelState.IsValid)
        {
            linkType.Id = Guid.NewGuid();
            _context.Add(linkType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(linkType);
    }

    // GET: LinkType/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var linkType = await _context.LinkTypes.FindAsync(id);
        if (linkType == null)
        {
            return NotFound();
        }
        return View(linkType);
    }

    // POST: LinkType/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id")] LinkType linkType)
    {
        if (id != linkType.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(linkType);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LinkTypeExists(linkType.Id))
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
        return View(linkType);
    }

    // GET: LinkType/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var linkType = await _context.LinkTypes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (linkType == null)
        {
            return NotFound();
        }

        return View(linkType);
    }

    // POST: LinkType/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var linkType = await _context.LinkTypes.FindAsync(id);
        if (linkType != null)
        {
            _context.LinkTypes.Remove(linkType);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool LinkTypeExists(Guid id)
    {
        return _context.LinkTypes.Any(e => e.Id == id);
    }
}