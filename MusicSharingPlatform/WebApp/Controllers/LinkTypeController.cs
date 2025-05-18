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
using App.BLL.DTO;
using App.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize(Roles = "admin")]

public class LinkTypeController : Controller
{
    private readonly IAppBLL _bll;

    public LinkTypeController(IAppBLL bll)
    {
        _bll = bll;
    }

    // GET: LinkType
    public async Task<IActionResult> Index()
    {
        return View(await _bll.LinkTypeService.AllAsync());
    }

    // GET: LinkType/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var linkType = await _bll.LinkTypeService.FindAsync(id.Value);
        
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

            _bll.LinkTypeService.Add(linkType);
            await _bll.SaveChangesAsync();

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

        var linkType = await _bll.LinkTypeService.FindAsync(id.Value);
        
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
            var linkType = await _bll.LinkTypeService.FindAsync(vm.Id);
            
            if (linkType == null)
            {
                return NotFound();
            }
            
            linkType.Name = vm.Name;
            
            _bll.LinkTypeService.Update(linkType);
            await _bll.SaveChangesAsync();
            
            
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

        var linkType = await _bll.LinkTypeService.FindAsync(id.Value);
        
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
        await _bll.LinkTypeService.RemoveAsync(id);
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}