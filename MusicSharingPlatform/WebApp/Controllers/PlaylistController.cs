
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Base.Helpers;
using App.BLL.DTO;
using App.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize]

public class PlaylistController : Controller
{
    private readonly IAppBLL _bll;

    public PlaylistController(IAppBLL bll)
    {
        _bll = bll;
    }

    // GET: Playlist
    public async Task<IActionResult> Index()
    {
        var playlists = await _bll.PlaylistService.AllAsync(User.GetUserId());
        
        return View(playlists);
    }

    // GET: Playlist/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var playlist = await _bll.PlaylistService.FindAsync(id.Value, User.GetUserId());
        
        
        if (playlist == null)
        {
            return NotFound();
        }

        return View(playlist);
    }

    // GET: Playlist/Create
    public async Task<IActionResult> Create()
    {
        var vm = new PlaylistViewModel { Playlist = new Playlist() };
        
        await PopulateSelectListsAsync(vm);
        
        return View(vm);
    }

    // POST: Playlist/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PlaylistViewModel vm)
    {
        if (ModelState.IsValid)
        {
            _bll.PlaylistService.Add(vm.Playlist);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        await PopulateSelectListsAsync(vm);
        
        return View(vm);
    }

    // GET: Playlist/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var playlist = await _bll.PlaylistService.FindAsync(id.Value, User.GetUserId());
        
        if (playlist == null)
        {
            return NotFound();
        }
        
        var vm = new PlaylistViewModel { Playlist = playlist };
        await PopulateSelectListsAsync(vm);

        return View(vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, PlaylistViewModel vm)
    {
        if (id != vm.Playlist.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _bll.PlaylistService.Update(vm.Playlist);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        await PopulateSelectListsAsync(vm);

        return View(vm);
    }

    // GET: Playlist/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var playlist = await _bll.PlaylistService.FindAsync(id.Value, User.GetUserId());
        
        if (playlist == null)
        {
            return NotFound();
        }

        return View(playlist);
    }
    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.PlaylistService.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateSelectListsAsync(PlaylistViewModel vm)
    {
        var userId = User.GetUserId();

        vm.ArtistsList = new SelectList(
            await _bll.ArtistService.AllAsync(userId),
            nameof(Artist.Id),
            nameof(Artist.DisplayName),
            vm.Playlist.UserId
        );
    }
}