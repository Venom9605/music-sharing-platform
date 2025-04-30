using App.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.DAL.EF.Repositories;
using App.DAL.Interfaces;
using Base.Helpers;
using App.BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;


namespace WebApp.Controllers;

[Authorize]
public class ArtistInTrackController : Controller
{
    
    private readonly IAppBLL _bll;

    public ArtistInTrackController(IAppBLL bll)
    {
        _bll = bll;
    }

    
    // GET: ArtistInTrack
    public async Task<IActionResult> Index()
    {
        var artistInTracks = await _bll.ArtistInTrackService.AllAsync(User.GetUserId());
        
        return View(artistInTracks);
    }

    
    
    // GET: ArtistInTrack/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var artistInTrack = await _bll.ArtistInTrackService.FindAsync(id.Value, User.GetUserId());
        
        if (artistInTrack == null)
        {
            return NotFound();
        }

        return View(artistInTrack);
    }

    // GET: ArtistInTrack/Create
    public async Task<IActionResult> Create()
    {
        var vm = new ArtistInTrackViewModel { ArtistInTrack = new ArtistInTrack() };
        await PopulateSelectListsAsync(vm);
        
        return View(vm);
    }

    // POST: ArtistInTrack/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ArtistInTrackViewModel vm)
    {
        if (ModelState.IsValid)
        {
            _bll.ArtistInTrackService.Add(vm.ArtistInTrack);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        await PopulateSelectListsAsync(vm);

        return View(vm);
    }


    // GET: ArtistInTrack/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var artistInTrack = await _bll.ArtistInTrackService.FindAsync(id.Value, User.GetUserId());
        
        if (artistInTrack == null)
        {
            return NotFound();
        }
        
        var vm = new ArtistInTrackViewModel { ArtistInTrack = artistInTrack };
        await PopulateSelectListsAsync(vm);

        return View(vm);
    }

    // POST: ArtistInTrack/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ArtistInTrackViewModel vm)
    {
        if (id != vm.ArtistInTrack.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _bll.ArtistInTrackService.Update(vm.ArtistInTrack);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        await PopulateSelectListsAsync(vm);

        return View(vm);
    }

    // GET: ArtistInTrack/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var artistInTrack = await _bll.ArtistInTrackService.FindAsync(id.Value, User.GetUserId());
        
        if (artistInTrack == null)
        {
            return NotFound();
        }

        return View(artistInTrack);
    }

    // POST: ArtistInTrack/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.ArtistInTrackService.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    private async Task PopulateSelectListsAsync(ArtistInTrackViewModel vm)
    {
        var userId = User.GetUserId();

        vm.TracksList = new SelectList(
            await _bll.TrackService.AllAsync(userId),
            nameof(Track.Id),
            nameof(Track.Title),
            vm.ArtistInTrack.TrackId
        );

        vm.ArtistRolesList = new SelectList(
            await _bll.ArtistRoleService.AllAsync(userId),
            nameof(ArtistRole.Id),
            nameof(ArtistRole.Name),
            vm.ArtistInTrack.ArtistRoleId
        );

        vm.ArtistsList = new SelectList(
            await _bll.ArtistService.AllAsync(userId),
            nameof(Artist.Id),
            nameof(Artist.DisplayName),
            vm.ArtistInTrack.UserId
        );
    }
}