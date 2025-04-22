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
using App.DAL.DTO;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize]

public class MoodController : Controller
{
    private readonly IAppUOW _uow;

    public MoodController(IAppUOW uow)
    {
        _uow = uow;
    }

    // GET: Mood
    public async Task<IActionResult> Index()
    {
        return View(await _uow.MoodRepository.AllAsync(User.GetUserId()));
    }

    // GET: Mood/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var mood = await _uow.MoodRepository.FindAsync(id.Value, User.GetUserId());
        
        if (mood == null)
        {
            return NotFound();
        }

        return View(mood);
    }

    // GET: Mood/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Mood/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MoodCreateViewModel vm)
    {
        if (ModelState.IsValid)
        {
            var mood = new Mood
            {
                Name = vm.Name
            };
            
            _uow.MoodRepository.Add(mood);
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }

    // GET: Mood/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var mood = await _uow.MoodRepository.FindAsync(id.Value, User.GetUserId());
        
        if (mood == null)
        {
            return NotFound();
        }
        
        var vm = new MoodEditViewModel
        {
            Id = mood.Id,
            Name = mood.Name
        };
        
        return View(vm);
    }

    // POST: Mood/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, MoodEditViewModel vm)
    {
        if (id != vm.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var mood = await _uow.MoodRepository.FindAsync(vm.Id, User.GetUserId());
            
            if (mood == null)
            {
                return NotFound();
            }
            
            mood.Name = vm.Name;
            _uow.MoodRepository.Update(mood);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }

    // GET: Mood/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var mood = await _uow.MoodRepository.FindAsync(id.Value, User.GetUserId());
        
        if (mood == null)
        {
            return NotFound();
        }

        return View(mood);
    }

    // POST: Mood/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _uow.MoodRepository.RemoveAsync(id, User.GetUserId());
        await _uow.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}