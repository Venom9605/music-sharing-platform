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

[Authorize(Roles = "admin")]

public class MoodsInTrackController : Controller
{
    private readonly IAppBLL _bll;

    public MoodsInTrackController(IAppBLL bll)
    {
        _bll = bll;
    }

    // GET: MoodsInTrack
    public async Task<IActionResult> Index()
    {
        var moodsInTracks = await _bll.MoodsInTrackService.AllAsync();
        
        return View(moodsInTracks);
    }

    // GET: MoodsInTrack/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var moodsInTrack = await _bll.MoodsInTrackService.FindAsync(id.Value);
        
        if (moodsInTrack == null)
        {
            return NotFound();
        }

        return View(moodsInTrack);
    }

    // GET: Playlist/Create
    public async Task<IActionResult> Create()
    {
        var vm = new MoodsInTrackViewModel { MoodsInTrack = new MoodsInTrack() };
        
        await PopulateSelectListsAsync(vm);
        
        return View(vm);
    }

    // POST: MoodsInTrack/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MoodsInTrackViewModel vm)
    {
        if (ModelState.IsValid)
        {
            _bll.MoodsInTrackService.Add(vm.MoodsInTrack);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        await PopulateSelectListsAsync(vm);
        
        return View(vm);
    }

    // GET: MoodsInTrack/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var moodsInTrack = await _bll.MoodsInTrackService.FindAsync(id.Value);
        
        if (moodsInTrack == null)
        {
            return NotFound();
        }
        
        var vm = new MoodsInTrackViewModel { MoodsInTrack = moodsInTrack };
        
        await PopulateSelectListsAsync(vm);
        
        return View(vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, MoodsInTrackViewModel vm)
    {
        if (id != vm.MoodsInTrack.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _bll.MoodsInTrackService.Update(vm.MoodsInTrack);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        await PopulateSelectListsAsync(vm);

        return View(vm);
    }

    // GET: MoodsInTrack/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var moodsInTrack = await _bll.MoodsInTrackService.FindAsync(id.Value);
        
        if (moodsInTrack == null)
        {
            return NotFound();
        }

        return View(moodsInTrack);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.MoodsInTrackService.RemoveAsync(id);
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateSelectListsAsync(MoodsInTrackViewModel vm)
    {
        

        vm.MoodsList = new SelectList(
            await _bll.MoodService.AllAsync(),
            nameof(Mood.Id),
            nameof(Mood.Name),
            vm.MoodsInTrack.MoodId
        );

        vm.TracksList = new SelectList(
            await _bll.TrackService.AllAsync(),
            nameof(Track.Id),
            nameof(Track.Title),
            vm.MoodsInTrack.TrackId
        );

    }
}