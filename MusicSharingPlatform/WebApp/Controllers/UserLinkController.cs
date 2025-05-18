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
using Artist = App.DTO.v1.Artist;

namespace WebApp.Controllers;

[Authorize(Roles = "admin")]

public class UserLinkController : Controller
{
    private readonly IAppBLL _bll;

    public UserLinkController(IAppBLL bll)
    {
        _bll = bll;
    }

    // GET: UserLink
    public async Task<IActionResult> Index()
    {
        var userLink = await _bll.UserLinkService.AllAsync();
        
        return View(userLink);
    }

    // GET: UserLink/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userLink = await _bll.UserLinkService.FindAsync(id.Value);
        
        
        if (userLink == null)
        {
            return NotFound();
        }

        return View(userLink);
    }

    // GET: UserLink/Create
    public async Task<IActionResult> Create()
    {
        var vm = new UserLinkViewModel() { UserLink = new UserLink() };
        
        await PopulateSelectListsAsync(vm);
        
        return View(vm);
    }

    // POST: UserLink/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserLinkViewModel vm)
    {
        if (ModelState.IsValid)
        {
            _bll.UserLinkService.Add(vm.UserLink);
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
        var userLink = await _bll.UserLinkService.FindAsync(id.Value);
        
        if (userLink == null)
        {
            return NotFound();
        }
        
        var vm = new UserLinkViewModel() { UserLink = userLink };
        await PopulateSelectListsAsync(vm);

        return View(vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, UserLinkViewModel vm)
    {
        if (id != vm.UserLink.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _bll.UserLinkService.Update(vm.UserLink);
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

        var userLink = await _bll.UserLinkService.FindAsync(id.Value);
        
        if (userLink == null)
        {
            return NotFound();
        }

        return View(userLink);
    }
    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.UserLinkService.RemoveAsync(id);
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateSelectListsAsync(UserLinkViewModel vm)
    {
        

        vm.UsersList = new SelectList(
            await _bll.ArtistService.AllAsync(),
            nameof(Artist.Id),
            nameof(Artist.DisplayName),
            vm.UserLink.UserId
        );
        
        vm.LinkTypesList = new SelectList(
            await _bll.LinkTypeService.AllAsync(),
            nameof(LinkType.Id),
            nameof(LinkType.Name),
            vm.UserLink.LinkTypeId
        );
    }
}