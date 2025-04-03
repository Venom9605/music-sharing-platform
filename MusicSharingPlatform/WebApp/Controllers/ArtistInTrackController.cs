using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.DAL.EF.Repositories;
using App.DAL.Interfaces;
using Base.Helpers;
using Domain;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;


namespace WebApp.Controllers;

[Authorize]
public class ArtistInTrackController : Controller
{
    private readonly IArtistInTrackRepository _artistInTrackRepository;
    private readonly ITrackRepository _trackRepository;
    private readonly IArtistRoleRepository _artistRoleRepository;
    private readonly IArtistRepository _artistRepository;


    public ArtistInTrackController(IArtistInTrackRepository artistInTrackRepository, ITrackRepository trackRepository, IArtistRoleRepository artistRoleRepository, IArtistRepository artistRepository)
    {
        _artistInTrackRepository = artistInTrackRepository;
        _trackRepository = trackRepository;
        _artistRoleRepository = artistRoleRepository;
        _artistRepository = artistRepository;
    }

    
    // GET: ArtistInTrack
    public async Task<IActionResult> Index()
    {
        return View(await _artistInTrackRepository.AllAsync(User.GetUserId()));
    }

    
    
    // GET: ArtistInTrack/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var artistInTrack = await _artistInTrackRepository.FindAsync(id.Value, User.GetUserId());
        
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
            _artistInTrackRepository.Add(vm.ArtistInTrack);
            await _artistInTrackRepository.SaveChangesAsync();
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

        var artistInTrack = await _artistInTrackRepository.FindAsync(id.Value, User.GetUserId());
        
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
            _artistInTrackRepository.Update(vm.ArtistInTrack);
            await _artistInTrackRepository.SaveChangesAsync();
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

        var artistInTrack = await _artistInTrackRepository.FindAsync(id.Value, User.GetUserId());
        
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
        await _artistInTrackRepository.RemoveAsync(id, User.GetUserId());
        await _artistInTrackRepository.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    private async Task PopulateSelectListsAsync(ArtistInTrackViewModel vm)
    {
        var userId = User.GetUserId();

        vm.TracksList = new SelectList(
            await _trackRepository.AllAsync(userId),
            nameof(Track.Id),
            nameof(Track.Title),
            vm.ArtistInTrack.TrackId
        );

        vm.ArtistRolesList = new SelectList(
            await _artistRoleRepository.AllAsync(userId),
            nameof(ArtistRole.Id),
            nameof(ArtistRole.Name),
            vm.ArtistInTrack.ArtistRoleId
        );

        vm.ArtistsList = new SelectList(
            await _artistRepository.AllAsync(userId),
            nameof(Artist.Id),
            nameof(Artist.DisplayName),
            vm.ArtistInTrack.UserId
        );
    }
}