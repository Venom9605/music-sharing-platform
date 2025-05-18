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

[Authorize(Roles = "admin")]

public class MoodController : Controller
{
    private readonly IAppBLL _bll;

    public MoodController(IAppBLL bll)
    {
        _bll = bll;
    }

    // GET: Mood
    public async Task<IActionResult> Index()
    {
        return View(await _bll.MoodService.AllAsync());
    }

    // GET: Mood/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var mood = await _bll.MoodService.FindAsync(id.Value);
        
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
            
            _bll.MoodService.Add(mood);
            await _bll.SaveChangesAsync();
            
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

        var mood = await _bll.MoodService.FindAsync(id.Value);
        
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
            var mood = await _bll.MoodService.FindAsync(vm.Id);
            
            if (mood == null)
            {
                return NotFound();
            }
            
            mood.Name = vm.Name;
            _bll.MoodService.Update(mood);
            await _bll.SaveChangesAsync();

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

        var mood = await _bll.MoodService.FindAsync(id.Value);
        
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
        await _bll.MoodService.RemoveAsync(id);
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}