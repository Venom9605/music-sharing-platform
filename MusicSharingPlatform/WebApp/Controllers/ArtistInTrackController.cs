using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.DAL.EF.Repositories;
using App.DAL.Interfaces;
using Base.Helpers;
using App.DAL.DTO;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;


namespace WebApp.Controllers;

[Authorize]
public class ArtistInTrackController : Controller
{
    
    private readonly IAppUOW _uow;

    public ArtistInTrackController(IAppUOW uow)
    {
        _uow = uow;
    }

    
    // GET: ArtistInTrack
    public async Task<IActionResult> Index()
    {
        return View(await _uow.ArtistInTrackRepository.AllAsync(User.GetUserId()));
    }

    
    
    // GET: ArtistInTrack/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var artistInTrack = await _uow.ArtistInTrackRepository.FindAsync(id.Value, User.GetUserId());
        
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
            _uow.ArtistInTrackRepository.Add(vm.ArtistInTrack);
            await _uow.SaveChangesAsync();
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

        var artistInTrack = await _uow.ArtistInTrackRepository.FindAsync(id.Value, User.GetUserId());
        
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
            _uow.ArtistInTrackRepository.Update(vm.ArtistInTrack);
            await _uow.SaveChangesAsync();
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

        var artistInTrack = await _uow.ArtistInTrackRepository.FindAsync(id.Value, User.GetUserId());
        
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
        await _uow.ArtistInTrackRepository.RemoveAsync(id, User.GetUserId());
        await _uow.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    private async Task PopulateSelectListsAsync(ArtistInTrackViewModel vm)
    {
        var userId = User.GetUserId();

        vm.TracksList = new SelectList(
            await _uow.TrackRepository.AllAsync(userId),
            nameof(Track.Id),
            nameof(Track.Title),
            vm.ArtistInTrack.TrackId
        );

        vm.ArtistRolesList = new SelectList(
            await _uow.ArtistRoleRepository.AllAsync(userId),
            nameof(ArtistRole.Id),
            nameof(ArtistRole.Name),
            vm.ArtistInTrack.ArtistRoleId
        );

        vm.ArtistsList = new SelectList(
            await _uow.ArtistRepository.AllAsync(userId),
            nameof(Artist.Id),
            nameof(Artist.DisplayName),
            vm.ArtistInTrack.UserId
        );
    }
}