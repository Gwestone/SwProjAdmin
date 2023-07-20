using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SwProjAdmin.Data;
using SwProjAdmin.Models;

namespace SwProjAdmin.Controllers
{
    public class GalleryItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GalleryItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GalleryItem
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GalleryItems.Include(g => g.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GalleryItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GalleryItems == null)
            {
                return NotFound();
            }

            var galleryItem = await _context.GalleryItems
                .Include(g => g.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (galleryItem == null)
            {
                return NotFound();
            }

            return View(galleryItem);
        }

        // GET: GalleryItem/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: GalleryItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,URL,ProductId")] GalleryItem galleryItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(galleryItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", galleryItem.ProductId);
            return View(galleryItem);
        }

        // GET: GalleryItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GalleryItems == null)
            {
                return NotFound();
            }

            var galleryItem = await _context.GalleryItems.FindAsync(id);
            if (galleryItem == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", galleryItem.ProductId);
            return View(galleryItem);
        }

        // POST: GalleryItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,URL,ProductId")] GalleryItem galleryItem)
        {
            if (id != galleryItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galleryItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryItemExists(galleryItem.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", galleryItem.ProductId);
            return View(galleryItem);
        }

        // GET: GalleryItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GalleryItems == null)
            {
                return NotFound();
            }

            var galleryItem = await _context.GalleryItems
                .Include(g => g.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (galleryItem == null)
            {
                return NotFound();
            }

            return View(galleryItem);
        }

        // POST: GalleryItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GalleryItems == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GalleryItems'  is null.");
            }
            var galleryItem = await _context.GalleryItems.FindAsync(id);
            if (galleryItem != null)
            {
                _context.GalleryItems.Remove(galleryItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalleryItemExists(int id)
        {
          return (_context.GalleryItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
