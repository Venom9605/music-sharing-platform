using App.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Base.Helpers;
using App.BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;
using Artist = App.DTO.v1.Artist;

namespace WebApp.Controllers;

[Authorize]

public class RatingController : Controller
{
    private readonly IAppBLL _bll;

    public RatingController(IAppBLL bll)
    {
        _bll = bll;
    }

    // GET: Rating
    public async Task<IActionResult> Index()
    {
        return View(await _bll.RatingService.AllAsync(User.GetUserId()));
    }

    // GET: Rating/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rating = await _bll.RatingService.FindAsync(id.Value, User.GetUserId());
        
        if (rating == null)
        {
            return NotFound();
        }

        return View(rating);
    }

    // GET: Rating/Create
    public async Task<IActionResult> Create()
    {
        var vm = new RatingsViewModel { Rating = new Rating() };
        
        await PopulateSelectListsAsync(vm);
        
        return View(vm);
    }

    // POST: Rating/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RatingsViewModel vm)
    {
        if (ModelState.IsValid)
        {
            _bll.RatingService.Add(vm.Rating);
            
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        await PopulateSelectListsAsync(vm);
        
        return View(vm);
    }

    // GET: Rating/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rating = await _bll.RatingService.FindAsync(id.Value, User.GetUserId());
        
        if (rating == null)
        {
            return NotFound();
        }
        
        var vm = new RatingsViewModel { Rating = rating };
        await PopulateSelectListsAsync(vm);
        
        
        return View(vm);
    }

    // POST: Rating/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, RatingsViewModel vm)
    {
        if (id != vm.Rating.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _bll.RatingService.Update(vm.Rating);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        await PopulateSelectListsAsync(vm);
        
        return View(vm);
    }

    // GET: Rating/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rating = await _bll.RatingService.FindAsync(id.Value, User.GetUserId());
        
        if (rating == null)
        {
            return NotFound();
        }

        return View(rating);
    }

    // POST: Rating/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.RatingService.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    
    private async Task PopulateSelectListsAsync(RatingsViewModel vm)
    {
        var userId = User.GetUserId();

        vm.TracksList = new SelectList(
            await _bll.TrackService.AllAsync(userId),
            nameof(Track.Id),
            nameof(Track.Title),
            vm.Rating.TrackId
        );
        
        vm.ArtistsList = new SelectList(
            await _bll.ArtistService.AllAsync(userId),
            nameof(Artist.Id),
            nameof(Artist.DisplayName),
            vm.Rating.UserId
        );
    }
}