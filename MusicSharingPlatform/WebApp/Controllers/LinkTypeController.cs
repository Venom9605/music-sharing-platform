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

public class LinkTypeController : Controller
{
    private readonly IAppUOW _uow;

    public LinkTypeController(IAppUOW uow)
    {
        _uow = uow;
    }

    // GET: LinkType
    public async Task<IActionResult> Index()
    {
        return View(await _uow.LinkTypeRepository.AllAsync(User.GetUserId()));
    }

    // GET: LinkType/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var linkType = await _uow.LinkTypeRepository.FindAsync(id.Value, User.GetUserId());
        
        if (linkType == null)
        {
            return NotFound();
        }

        return View(linkType);
    }

    // GET: LinkType/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: LinkType/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LinkTypeCreateViewModel vm)
    {
        if (ModelState.IsValid)
        {
            var linkType = new LinkType
            {
                Name = vm.Name
            };

            _uow.LinkTypeRepository.Add(linkType);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }

    // GET: LinkType/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var linkType = await _uow.LinkTypeRepository.FindAsync(id.Value, User.GetUserId());
        
        if (linkType == null)
        {
            return NotFound();
        }

        var vm = new LinkTypeEditViewModel()
        {
            Id = linkType.Id,
            Name = linkType.Name
        };
        
        return View(vm);
    }

    // POST: LinkType/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, LinkTypeEditViewModel vm)
    {
        if (id != vm.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var linkType = await _uow.LinkTypeRepository.FindAsync(vm.Id, User.GetUserId());
            
            if (linkType == null)
            {
                return NotFound();
            }
            
            linkType.Name = vm.Name;
            
            _uow.LinkTypeRepository.Update(linkType);
            await _uow.SaveChangesAsync();
            
            
            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }

    // GET: LinkType/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var linkType = await _uow.LinkTypeRepository.FindAsync(id.Value, User.GetUserId());
        
        if (linkType == null)
        {
            return NotFound();
        }

        return View(linkType);
    }

    // POST: LinkType/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _uow.LinkTypeRepository.RemoveAsync(id, User.GetUserId());
        await _uow.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}