using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using Domain;

namespace WebApp.Controllers
{
    public class ArtistRoleController : Controller
    {
        private readonly AppDbContext _context;

        public ArtistRoleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ArtistRole
        public async Task<IActionResult> Index()
        {
            return View(await _context.ArtistRoles.ToListAsync());
        }

        // GET: ArtistRole/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistRole = await _context.ArtistRoles
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("Name,Id")] ArtistRole artistRole)
        {
            if (ModelState.IsValid)
            {
                artistRole.Id = Guid.NewGuid();
                _context.Add(artistRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artistRole);
        }

        // GET: ArtistRole/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistRole = await _context.ArtistRoles.FindAsync(id);
            if (artistRole == null)
            {
                return NotFound();
            }
            return View(artistRole);
        }

        // POST: ArtistRole/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id")] ArtistRole artistRole)
        {
            if (id != artistRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artistRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistRoleExists(artistRole.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(artistRole);
        }

        // GET: ArtistRole/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistRole = await _context.ArtistRoles
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var artistRole = await _context.ArtistRoles.FindAsync(id);
            if (artistRole != null)
            {
                _context.ArtistRoles.Remove(artistRole);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistRoleExists(Guid id)
        {
            return _context.ArtistRoles.Any(e => e.Id == id);
        }
    }
}
