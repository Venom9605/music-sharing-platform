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

public class ArtistRoleController : Controller
{
    
    private readonly IAppUOW _uow;

    public ArtistRoleController(IAppUOW uow)
    {
        _uow = uow;
    }

    // GET: ArtistRole
    public async Task<IActionResult> Index()
    {
        return View(await _uow.ArtistRoleRepository.AllAsync(User.GetUserId()));
    }

    // GET: ArtistRole/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var artistRole = await _uow.ArtistRoleRepository.FindAsync(id.Value, User.GetUserId());
        
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
            
            _uow.ArtistRoleRepository.Add(artistRole);
            await _uow.SaveChangesAsync();
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

        var artistRole = await _uow.ArtistRoleRepository.FindAsync(id.Value, User.GetUserId());
        
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
            var artistRole = await _uow.ArtistRoleRepository.FindAsync(vm.Id, User.GetUserId());
            
            if (artistRole == null)
            {
                return NotFound();
            }
            
            artistRole.Name = vm.Name;
            
            _uow.ArtistRoleRepository.Update(artistRole);
            await _uow.SaveChangesAsync();
            
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

        var artistRole = await _uow.ArtistRoleRepository.FindAsync(id.Value, User.GetUserId());
        
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
        await _uow.ArtistRoleRepository.RemoveAsync(id, User.GetUserId());
        await _uow.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}