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

public class TagController : Controller
{
    private readonly IAppBLL _bll;

    public TagController(IAppBLL bll)
    {
        _bll = bll;
    }

    // GET: Tag
    public async Task<IActionResult> Index()
    {
        return View(await _bll.TagService.AllAsync(User.GetUserId()));
    }

    // GET: Tag/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tag = await _bll.TagService.FindAsync(id.Value, User.GetUserId());
        
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
            
            _bll.TagService.Add(tag);
            await _bll.SaveChangesAsync();
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

        var tag = await _bll.TagService.FindAsync(id.Value, User.GetUserId());
        
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
            var tag = await _bll.TagService.FindAsync(vm.Id, User.GetUserId());
            
            if (tag == null)
            {
                return NotFound();
            }
            
            tag.Name = vm.Name;
            
            _bll.TagService.Update(tag);
            await _bll.SaveChangesAsync();
            
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

        var tag = await _bll.TagService.FindAsync(id.Value, User.GetUserId());
        
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
        await _bll.TagService.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
}