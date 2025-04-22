using Microsoft.AspNetCore.Mvc;
using App.DAL.Interfaces;
using Base.Helpers;
using App.DAL.DTO;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers;


[Authorize]
public class ArtistController : Controller
{

    private readonly IAppUOW _uow;

    public ArtistController(IAppUOW uow)
    {
        _uow = uow;
    }

    // GET: Artist
    public async Task<IActionResult> Index()
    {
        return View(await _uow.ArtistRepository.AllAsync(User.GetUserId()));
    }

    // GET: Artist/Edit/5
    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var artist = await _uow.ArtistRepository.FindAsync(id, User.GetUserId());
        
        if (artist == null)
        {
            return NotFound();
        }
        
        var vm = new ArtistEditViewModel
        {
            Id = artist.Id,
            DisplayName = artist.DisplayName,
            Bio = artist.Bio,
            ProfilePicture = artist.ProfilePicture
        };
        
        return View(vm);
    }

    // POST: Artist/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, ArtistEditViewModel vm)
    {
        if (id != vm.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var artist = await _uow.ArtistRepository.FindAsync(vm.Id, User.GetUserId());
            
            if (artist == null)
            {
                return NotFound();
            }
            
            artist.DisplayName = vm.DisplayName;
            artist.Bio = vm.Bio;
            artist.ProfilePicture = vm.ProfilePicture;
            
            _uow.ArtistRepository.Update(artist);
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }
}