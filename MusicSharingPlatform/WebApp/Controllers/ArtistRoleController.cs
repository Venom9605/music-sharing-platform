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

public class ArtistRoleController : Controller
{
    
    private readonly IAppBLL _bll;

    public ArtistRoleController(IAppBLL bll)
    {
        _bll = bll;
    }

    // GET: ArtistRole
    public async Task<IActionResult> Index()
    {
        _bll.ArtistRoleService.CustomMethodTest();
        return View(await _bll.ArtistRoleService.AllAsync());
    }

    // GET: ArtistRole/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var artistRole = await _bll.ArtistRoleService.FindAsync(id.Value);
        
        if (artistRole == null)
        {
            return NotFound();
        }

        return View(artistRole);
    }

    // GET: ArtistRole/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ArtistRole/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ArtistRoleCreateViewModel vm)
    {
        if (ModelState.IsValid)
        {
            var artistRole = new ArtistRole
            {
                Name = vm.Name
            };
            
            _bll.ArtistRoleService.Add(artistRole);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        return View(vm);
    }

    // GET: ArtistRole/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var artistRole = await _bll.ArtistRoleService.FindAsync(id.Value);
        
        if (artistRole == null)
        {
            return NotFound();
        }
        
        var vm = new ArtistRoleEditViewModel
        {
            Id = artistRole.Id,
            Name = artistRole.Name
        };
        
        return View(vm);
    }

    // POST: ArtistRole/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ArtistRoleEditViewModel vm)
    {
        if (id != vm.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var artistRole = await _bll.ArtistRoleService.FindAsync(vm.Id);
            
            if (artistRole == null)
            {
                return NotFound();
            }
            
            artistRole.Name = vm.Name;
            
            _bll.ArtistRoleService.Update(artistRole);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }

    // GET: ArtistRole/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var artistRole = await _bll.ArtistRoleService.FindAsync(id.Value);
        
        if (artistRole == null)
        {
            return NotFound();
        }

        return View(artistRole);
    }

    // POST: ArtistRole/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.ArtistRoleService.RemoveAsync(id);
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}