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
using App.BLL.DTO;
using App.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize]

public class TrackLinkController : Controller
{
    private readonly IAppBLL _bll;

    public TrackLinkController(IAppBLL bll)
    {
        _bll = bll;
    }

    // GET: TrackLink
    public async Task<IActionResult> Index()
    {
        var trackLink = await _bll.TrackLinkService.AllAsync(User.GetUserId());
        
        return View(trackLink);
    }

    // GET: TrackLink/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var trackLink = await _bll.TrackLinkService.FindAsync(id.Value, User.GetUserId());
        
        
        if (trackLink == null)
        {
            return NotFound();
        }

        return View(trackLink);
    }

    // GET: TrackLink/Create
    public async Task<IActionResult> Create()
    {
        var vm = new TrackLinkViewModel() { TrackLink = new TrackLink() };
        
        await PopulateSelectListsAsync(vm);
        
        return View(vm);
    }

    // POST: TrackLink/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TrackLinkViewModel vm)
    {
        if (ModelState.IsValid)
        {
            _bll.TrackLinkService.Add(vm.TrackLink);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        await PopulateSelectListsAsync(vm);
        
        return View(vm);
    }

    // GET: TrackLink/Edit/5

    // POST: TrackLink/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var trackLink = await _bll.TrackLinkService.FindAsync(id.Value, User.GetUserId());
        
        if (trackLink == null)
        {
            return NotFound();
        }
        
        var vm = new TrackLinkViewModel() { TrackLink = trackLink };
        await PopulateSelectListsAsync(vm);

        return View(vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, TrackLinkViewModel vm)
    {
        if (id != vm.TrackLink.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _bll.TrackLinkService.Update(vm.TrackLink);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        await PopulateSelectListsAsync(vm);

        return View(vm);
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var trackLink = await _bll.TrackLinkService.FindAsync(id.Value, User.GetUserId());
        
        if (trackLink == null)
        {
            return NotFound();
        }

        return View(trackLink);
    }
    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.TrackLinkService.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateSelectListsAsync(TrackLinkViewModel vm)
    {
        var userId = User.GetUserId();

        vm.TracksList = new SelectList(
            await _bll.TrackService.AllAsync(userId),
            nameof(Track.Id),
            nameof(Track.Title),
            vm.TrackLink.TrackId
        );
        
        vm.LinkTypesList = new SelectList(
            await _bll.LinkTypeService.AllAsync(userId),
            nameof(LinkType.Id),
            nameof(LinkType.Name),
            vm.TrackLink.LinkTypeId
        );
    }
}