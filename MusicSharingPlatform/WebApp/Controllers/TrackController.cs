using Microsoft.AspNetCore.Mvc;
using App.DAL.Interfaces;
using Base.Helpers;
using Domain;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize]

public class TrackController : Controller
{

    private readonly IAppUOW _uow;

    public TrackController(IAppUOW uow)
    {
        _uow = uow;
    }

    // GET: Track
    public async Task<IActionResult> Index()
    {
        return View(await _uow.TrackRepository.AllAsync(User.GetUserId()));
    }

    // GET: Track/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var track = await _uow.TrackRepository.FindAsync(id.Value, User.GetUserId());

        if (track == null)
        {
            return NotFound();
        }

        return View(track);
    }

    // GET: Track/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Track/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TrackCreateViewModel vm)
    {
        if (ModelState.IsValid)
        {
            var entity = new Track
            {
                Title = vm.Title,
                FilePath = vm.FilePath,
                CoverPath = vm.CoverPath,
                Duration = vm.Duration,
                Uploaded = DateTime.UtcNow,
                TimesPlayed = 0,
                TimesSaved = 0
                
            };

            _uow.TrackRepository.Add(entity);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }

    // GET: Track/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var track = await _uow.TrackRepository.FindAsync(id.Value, User.GetUserId());
        
        if (track == null)
        {
            return NotFound();
        }
        
        var vm = new TrackEditViewModel
        {
            Id = track.Id,
            Title = track.Title,
            FilePath = track.FilePath,
            CoverPath = track.CoverPath,
            Duration = track.Duration
        };
        
        return View(vm);
    }

    // POST: Track/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, TrackEditViewModel vm)
    {
        if (id != vm.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var track = await _uow.TrackRepository.FindAsync(vm.Id, User.GetUserId());
            
            if (track == null)
            {
                return NotFound();
            }
            
            track.Title = vm.Title;
            track.FilePath = vm.FilePath;
            track.CoverPath = vm.CoverPath;
            track.Duration = vm.Duration;
            
            _uow.TrackRepository.Update(track);
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }

    // GET: Track/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var track = await _uow.TrackRepository.FindAsync(id.Value, User.GetUserId());
        
        if (track == null)
        {
            return NotFound();
        }
        
        return View(track);
    }

    // POST: Track/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _uow.TrackRepository.RemoveAsync(id, User.GetUserId());
        await _uow.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
}