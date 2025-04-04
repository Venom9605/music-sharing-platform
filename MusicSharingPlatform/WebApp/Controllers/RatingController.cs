using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.DAL.Interfaces;
using Base.Helpers;
using Domain;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize]

public class RatingController : Controller
{
    private readonly IAppUOW _uow;

    public RatingController(IAppUOW uow)
    {
        _uow = uow;
    }

    // GET: Rating
    public async Task<IActionResult> Index()
    {
        return View(await _uow.RatingRepository.AllAsync(User.GetUserId()));
    }

    // GET: Rating/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rating = await _uow.RatingRepository.FindAsync(id.Value, User.GetUserId());
        
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
                await _uow.TrackRepository.AllAsync(User.GetUserId()),
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
            vm.Rating.Date = DateTime.UtcNow;

            _uow.RatingRepository.Add(vm.Rating);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        
        vm.TracksList = new SelectList(
            await _uow.TrackRepository.AllAsync(User.GetUserId()),
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

        var rating = await _uow.RatingRepository.FindAsync(id.Value, User.GetUserId());
        
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
            var dbEntity = await _uow.RatingRepository.FindAsync(id, User.GetUserId());
            if (dbEntity == null) return NotFound();

            dbEntity.Score = rating.Score;
            dbEntity.Comment = rating.Comment;

            _uow.RatingRepository.Update(dbEntity);
            await _uow.SaveChangesAsync();

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

        var rating = await _uow.RatingRepository.FindAsync(id.Value, User.GetUserId());
        
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
        await _uow.RatingRepository.RemoveAsync(id, User.GetUserId());
        await _uow.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}