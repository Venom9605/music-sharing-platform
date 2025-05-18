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

public class TagsInPlaylistController : Controller
{
    private readonly IAppBLL _bll;

    public TagsInPlaylistController(IAppBLL bll)
    {
        _bll = bll;
    }

    // GET: TagsInPlaylist
    public async Task<IActionResult> Index()
    {
        var tagsInPlaylists = await _bll.TagsInPlaylistService.AllAsync();
        
        return View(tagsInPlaylists);
    }

    // GET: TagsInPlaylist/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tagsInPlaylists = await _bll.TagsInPlaylistService.FindAsync(id.Value);
        
        
        if (tagsInPlaylists == null)
        {
            return NotFound();
        }

        return View(tagsInPlaylists);
    }

    // GET: TagsInPlaylist/Create
    public async Task<IActionResult> Create()
    {
        var vm = new TagsInPlaylistViewModel() { TagsInPlaylist = new TagsInPlaylist() };
        
        await PopulateSelectListsAsync(vm);
        
        return View(vm);
    }

    // POST: TagsInPlaylist/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TagsInPlaylistViewModel vm)
    {
        if (ModelState.IsValid)
        {
            _bll.TagsInPlaylistService.Add(vm.TagsInPlaylist);
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
        
        var tagsInPlaylist = await _bll.TagsInPlaylistService.FindAsync(id.Value);
        
        if (tagsInPlaylist == null)
        {
            return NotFound();
        }
        
        var vm = new TagsInPlaylistViewModel() { TagsInPlaylist = tagsInPlaylist };
        await PopulateSelectListsAsync(vm);

        return View(vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, TagsInPlaylistViewModel vm)
    {
        if (id != vm.TagsInPlaylist.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _bll.TagsInPlaylistService.Update(vm.TagsInPlaylist);
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

        var tagsInPlaylist = await _bll.TagsInPlaylistService.FindAsync(id.Value);
        
        if (tagsInPlaylist == null)
        {
            return NotFound();
        }

        return View(tagsInPlaylist);
    }
    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.TagsInPlaylistService.RemoveAsync(id);
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateSelectListsAsync(TagsInPlaylistViewModel vm)
    {
        

        vm.TagsList = new SelectList(
            await _bll.TagService.AllAsync(),
            nameof(Tag.Id),
            nameof(Tag.Name),
            vm.TagsInPlaylist.TagId
        );
        
        vm.PlaylistsList = new SelectList(
            await _bll.PlaylistService.AllAsync(),
            nameof(Playlist.Id),
            nameof(Playlist.Name),
            vm.TagsInPlaylist.PlaylistId
        );
    }
}