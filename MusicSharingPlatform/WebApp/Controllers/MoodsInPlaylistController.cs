using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.DAL.Interfaces;
using Base.Helpers;
using App.BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize(Roles = "admin")]

public class MoodsInPlaylistController : Controller
{
    private readonly IAppBLL _bll;


    public MoodsInPlaylistController(IAppBLL bll)
    {
        _bll = bll;
    }

    // GET: MoodsInPlaylist
    public async Task<IActionResult> Index()
    {
        return View(await _bll.MoodsInPlaylistService.AllAsync());
    }

    // GET: MoodsInPlaylist/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var moodsInPlaylist = await _bll.MoodsInPlaylistService.FindAsync(id.Value);
        
        if (moodsInPlaylist == null)
        {
            return NotFound();
        }

        return View(moodsInPlaylist);
    }

    // GET: MoodsInPlaylist/Create
    public async Task<IActionResult> Create()
    {
        var vm = new MoodsInPlaylistViewModel { MoodsInPlaylist = new MoodsInPlaylist() };
        await PopulateSelectListsAsync(vm);
        
        return View(vm);
    }

    // POST: MoodsInPlaylist/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MoodsInPlaylistViewModel vm)
    {
        if (ModelState.IsValid)
        {
            _bll.MoodsInPlaylistService.Add(vm.MoodsInPlaylist);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        await PopulateSelectListsAsync(vm);
        return View(vm);
    }

    // GET: MoodsInPlaylist/Edit/5

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var moodsInPlaylist = await _bll.MoodsInPlaylistService.FindAsync(id.Value);
        
        if (moodsInPlaylist == null)
        {
            return NotFound();
        }
        
        var vm = new MoodsInPlaylistViewModel() { MoodsInPlaylist = moodsInPlaylist };
        await PopulateSelectListsAsync(vm);

        return View(vm);
    }
    

    // POST: MoodsInPlaylist/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, MoodsInPlaylistViewModel vm)
    {
        if (id != vm.MoodsInPlaylist.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _bll.MoodsInPlaylistService.Update(vm.MoodsInPlaylist);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        await PopulateSelectListsAsync(vm);

        return View(vm);
    }

    // GET: MoodsInPlaylist/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var moodsInPlaylist = await _bll.MoodsInPlaylistService.FindAsync(id.Value);
        
        if (moodsInPlaylist == null)
        {
            return NotFound();
        }

        return View(moodsInPlaylist);
    }

    // POST: ArtistInTrack/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.MoodsInPlaylistService.RemoveAsync(id);
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    
    private async Task PopulateSelectListsAsync(MoodsInPlaylistViewModel vm)
    {
        

        vm.MoodsList = new SelectList(
            await _bll.MoodService.AllAsync(),
            nameof(Mood.Id),
            nameof(Mood.Name),
            vm.MoodsInPlaylist.MoodId
        );

        vm.PlaylistsList = new SelectList(
            await _bll.PlaylistService.AllAsync(),
            nameof(Playlist.Id),
            nameof(Playlist.Name),
            vm.MoodsInPlaylist.PlaylistId
        );
    }
}