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
        var vm = new RatingsViewModel
        {
            Rating = new Rating(),
            TracksList = new SelectList(
                await _bll.TrackService.AllAsync(User.GetUserId()),
                nameof(Track.Id),
                nameof(Track.Title)
            )
        };
        
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
            vm.Rating.UserId = User.GetUserId();

            _bll.RatingService.Add(vm.Rating);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        
        vm.TracksList = new SelectList(
            await _bll.TrackService.AllAsync(User.GetUserId()),
            nameof(Track.Id),
            nameof(Track.Title),
            vm.Rating.TrackId
        );
        
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
        
        return View(rating);
    }

    // POST: Rating/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, Rating rating)
    {
        if (id != rating.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var dbEntity = await _bll.RatingService.FindAsync(id, User.GetUserId());
            if (dbEntity == null) return NotFound();

            dbEntity.Score = rating.Score;
            dbEntity.Comment = rating.Comment;

            _bll.RatingService.Update(dbEntity);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        
        return View(rating);
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
}