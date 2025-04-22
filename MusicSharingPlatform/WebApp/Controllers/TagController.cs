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

public class TagController : Controller
{
    private readonly IAppUOW _uow;

    public TagController(IAppUOW uow)
    {
        _uow = uow;
    }

    // GET: Tag
    public async Task<IActionResult> Index()
    {
        return View(await _uow.TagRepository.AllAsync(User.GetUserId()));
    }

    // GET: Tag/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tag = await _uow.TagRepository.FindAsync(id.Value, User.GetUserId());
        
        if (tag == null)
        {
            return NotFound();
        }

        return View(tag);
    }

    // GET: Tag/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Tag/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TagCreateViewModel vm)
    {
        if (ModelState.IsValid)
        {
            var tag = new Tag
            {
                Name = vm.Name
            };
            
            _uow.TagRepository.Add(tag);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }

    // GET: Tag/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tag = await _uow.TagRepository.FindAsync(id.Value, User.GetUserId());
        
        if (tag == null)
        {
            return NotFound();
        }
        
        var vm = new TagEditViewModel
        {
            Id = tag.Id,
            Name = tag.Name
        };
        
        return View(vm);
    }

    // POST: Tag/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, TagEditViewModel vm)
    {
        if (id != vm.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var tag = await _uow.TagRepository.FindAsync(vm.Id, User.GetUserId());
            
            if (tag == null)
            {
                return NotFound();
            }
            
            tag.Name = vm.Name;
            
            _uow.TagRepository.Update(tag);
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }

    // GET: Tag/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tag = await _uow.TagRepository.FindAsync(id.Value, User.GetUserId());
        
        if (tag == null)
        {
            return NotFound();
        }

        return View(tag);
    }

    // POST: Tag/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _uow.TagRepository.RemoveAsync(id, User.GetUserId());
        await _uow.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
}