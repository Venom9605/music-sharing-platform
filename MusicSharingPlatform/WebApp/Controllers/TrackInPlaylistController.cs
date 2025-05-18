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

public class TrackInPlaylistController : Controller
{
    private readonly IAppBLL _bll;

    public TrackInPlaylistController(IAppBLL bll)
    {
        _bll = bll;
    }

    // GET: TrackInPlaylist
    public async Task<IActionResult> Index()
    {
        var tracksInPlaylist = await _bll.TrackInPlaylistService.AllAsync();
        
        return View(tracksInPlaylist);
    }

    // GET: TrackInPlaylist/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var trackInPlaylist = await _bll.TrackInPlaylistService.FindAsync(id.Value);
        
        
        if (trackInPlaylist == null)
        {
            return NotFound();
        }

        return View(trackInPlaylist);
    }

    // GET: TrackInPlaylist/Create
    public async Task<IActionResult> Create()
    {
        var vm = new TrackInPlaylistViewModel() { TrackInPlaylist = new TrackInPlaylist() };
        
        await PopulateSelectListsAsync(vm);
        
        return View(vm);
    }

    // POST: TrackInPlaylist/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TrackInPlaylistViewModel vm)
    {
        if (ModelState.IsValid)
        {
            _bll.TrackInPlaylistService.Add(vm.TrackInPlaylist);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        await PopulateSelectListsAsync(vm);
        
        return View(vm);
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var trackInPlaylist = await _bll.TrackInPlaylistService.FindAsync(id.Value);
        
        if (trackInPlaylist == null)
        {
            return NotFound();
        }
        
        var vm = new TrackInPlaylistViewModel { TrackInPlaylist = trackInPlaylist };
        await PopulateSelectListsAsync(vm);

        return View(vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, TrackInPlaylistViewModel vm)
    {
        if (id != vm.TrackInPlaylist.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _bll.TrackInPlaylistService.Update(vm.TrackInPlaylist);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        await PopulateSelectListsAsync(vm);

        return View(vm);
    }

    // GET: TrackInPlaylist/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var trackInPlaylist = await _bll.TrackInPlaylistService.FindAsync(id.Value);
        
        if (trackInPlaylist == null)
        {
            return NotFound();
        }

        return View(trackInPlaylist);
    }
    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.TrackInPlaylistService.RemoveAsync(id);
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateSelectListsAsync(TrackInPlaylistViewModel vm)
    {
        

        vm.TracksList = new SelectList(
            await _bll.TrackService.AllAsync(),
            nameof(Track.Id),
            nameof(Track.Title),
            vm.TrackInPlaylist.TrackId
        );
        
        vm.PlaylistsList = new SelectList(
            await _bll.PlaylistService.AllAsync(),
            nameof(Playlist.Id),
            nameof(Playlist.Name),
            vm.TrackInPlaylist.PlaylistId
        );
    }
}