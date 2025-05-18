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
using Microsoft.Identity.Client;
using WebApp.ViewModels;
using Artist = App.DTO.v1.Artist;

namespace WebApp.Controllers;

[Authorize(Roles = "admin")]

public class UserSavedTrackController : Controller
{
    private readonly IAppBLL _bll;

    public UserSavedTrackController(IAppBLL bll)
    {
        _bll = bll;
    }

    // GET: UserSavedTrack
    public async Task<IActionResult> Index()
    {
        var userSavedTracks = await _bll.UserSavedTracksService.AllAsync();
        
        return View(userSavedTracks);
    }

    // GET: UserSavedTrack/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userSavedTrack = await _bll.UserSavedTracksService.FindAsync(id.Value);
        
        
        if (userSavedTrack == null)
        {
            return NotFound();
        }

        return View(userSavedTrack);
    }

    // GET: UserSavedTrack/Create
    public async Task<IActionResult> Create()
    {
        var vm = new UserSavedTrackViewModel() { UserSavedTrack = new UserSavedTracks() };
        
        await PopulateSelectListsAsync(vm);
        
        return View(vm);
    }

    // POST: UserSavedTrack/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserSavedTrackViewModel vm)
    {
        if (ModelState.IsValid)
        {
            _bll.UserSavedTracksService.Add(vm.UserSavedTrack);
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
        var userSavedTrack = await _bll.UserSavedTracksService.FindAsync(id.Value);
        
        if (userSavedTrack == null)
        {
            return NotFound();
        }
        
        var vm = new UserSavedTrackViewModel() { UserSavedTrack = userSavedTrack };
        await PopulateSelectListsAsync(vm);

        return View(vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, UserSavedTrackViewModel vm)
    {
        if (id != vm.UserSavedTrack.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _bll.UserSavedTracksService.Update(vm.UserSavedTrack);
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

        var userSavedTrack = await _bll.UserSavedTracksService.FindAsync(id.Value);
        
        if (userSavedTrack == null)
        {
            return NotFound();
        }

        return View(userSavedTrack);
    }
    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.UserSavedTracksService.RemoveAsync(id);
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateSelectListsAsync(UserSavedTrackViewModel vm)
    {
        

        vm.UsersList = new SelectList(
            await _bll.ArtistService.AllAsync(),
            nameof(Artist.Id),
            nameof(Artist.DisplayName),
            vm.UserSavedTrack.UserId
        );
        
        vm.TracksList = new SelectList(
            await _bll.TrackService.AllAsync(),
            nameof(Track.Id),
            nameof(Track.Title),
            vm.UserSavedTrack.TrackId
        );
    }
}