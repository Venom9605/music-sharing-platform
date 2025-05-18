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

public class TagsInTrackController : Controller
{
    private readonly IAppBLL _bll;

    public TagsInTrackController(IAppBLL bll)
    {
        _bll = bll;
    }

    // GET: TagsInTrack
    public async Task<IActionResult> Index()
    {
        var tagsInTrack = await _bll.TagsInTrackService.AllAsync();
        
        return View(tagsInTrack);
    }

    // GET: TagsInTrack/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tagsInTrack = await _bll.TagsInTrackService.FindAsync(id.Value);
        
        
        if (tagsInTrack == null)
        {
            return NotFound();
        }

        return View(tagsInTrack);
    }

    // GET: TagsInTrack/Create
    public async Task<IActionResult> Create()
    {
        var vm = new TagsInTrackViewModel() { TagsInTrack = new TagsInTrack() };
        
        await PopulateSelectListsAsync(vm);
        
        return View(vm);
    }

    // POST: TagsInTrack/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TagsInTrackViewModel vm)
    {
        if (ModelState.IsValid)
        {
            _bll.TagsInTrackService.Add(vm.TagsInTrack);
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
        var tagsInTrack = await _bll.TagsInTrackService.FindAsync(id.Value);
        
        if (tagsInTrack == null)
        {
            return NotFound();
        }
        
        var vm = new TagsInTrackViewModel() { TagsInTrack = tagsInTrack };
        await PopulateSelectListsAsync(vm);

        return View(vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, TagsInTrackViewModel vm)
    {
        if (id != vm.TagsInTrack.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _bll.TagsInTrackService.Update(vm.TagsInTrack);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        await PopulateSelectListsAsync(vm);

        return View(vm);
    }

    // GET: TagsInTrack/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tagsInTrack = await _bll.TagsInTrackService.FindAsync(id.Value);
        
        if (tagsInTrack == null)
        {
            return NotFound();
        }

        return View(tagsInTrack);
    }
    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.TagsInTrackService.RemoveAsync(id);
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateSelectListsAsync(TagsInTrackViewModel vm)
    {
        

        vm.TagsList = new SelectList(
            await _bll.TagService.AllAsync(),
            nameof(Tag.Id),
            nameof(Tag.Name),
            vm.TagsInTrack.TagId
        );
        
        vm.TracksList = new SelectList(
            await _bll.TrackService.AllAsync(),
            nameof(Track.Id),
            nameof(Track.Title),
            vm.TagsInTrack.TrackId
        );
    }
}