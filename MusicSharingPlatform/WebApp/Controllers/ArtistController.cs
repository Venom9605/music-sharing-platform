using App.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using App.DAL.Interfaces;
using Base.Helpers;
using App.DAL.DTO;
using App.DAL.EF.DataSeeding;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp.ViewModels;
using Artist = Domain.Artist;

namespace WebApp.Controllers;


[Authorize(Roles = "admin")]
public class ArtistController : Controller
{

    private readonly IAppBLL _bll;
    private readonly UserManager<Domain.Artist> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ArtistController(IAppBLL bll, RoleManager<IdentityRole> roleManager, UserManager<Artist> userManager)
    {
        _bll = bll;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    // GET: Artist
    public async Task<IActionResult> Index()
    {
        var artists = (await _bll.ArtistService.AllAsync()).ToList();

        foreach (var artist in artists)
        {
            artist.IsAdmin = await _userManager.IsInRoleAsync(
                new Domain.Artist { Id = artist.Id }, InitialData.AdminRoleName);
        }

        return View(artists);
    }

    // GET: Artist/Edit/5
    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var artist = await _bll.ArtistService.FindAsync(id);
        
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
            var artist = await _bll.ArtistService.FindAsync(vm.Id);
            
            if (artist == null)
            {
                return NotFound();
            }
            
            artist.DisplayName = vm.DisplayName;
            artist.Bio = vm.Bio;
            artist.ProfilePicture = vm.ProfilePicture;
            
            _bll.ArtistService.Update(artist);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }
    
    [HttpPost]
    public async Task<IActionResult> ToggleAdmin(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        var isAdmin = await _userManager.IsInRoleAsync(user, InitialData.AdminRoleName);

        IdentityResult result;
        if (isAdmin)
            result = await _userManager.RemoveFromRoleAsync(user, InitialData.AdminRoleName);
        else
            result = await _userManager.AddToRoleAsync(user, InitialData.AdminRoleName);

        if (!result.Succeeded) return BadRequest("Role update failed");

        return RedirectToAction(nameof(Index));
    }
}