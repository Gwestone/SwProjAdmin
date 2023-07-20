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
    public class AttributeSetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttributeSetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AttributeSet
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AttributeSets.Include(a => a.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AttributeSet/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.AttributeSets == null)
            {
                return NotFound();
            }

            var attributeSet = await _context.AttributeSets
                .Include(a => a.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attributeSet == null)
            {
                return NotFound();
            }

            return View(attributeSet);
        }

        // GET: AttributeSet/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: AttributeSet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,ProductId")] AttributeSet attributeSet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attributeSet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", attributeSet.ProductId);
            return View(attributeSet);
        }

        // GET: AttributeSet/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.AttributeSets == null)
            {
                return NotFound();
            }

            var attributeSet = await _context.AttributeSets.FindAsync(id);
            if (attributeSet == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", attributeSet.ProductId);
            return View(attributeSet);
        }

        // POST: AttributeSet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Type,ProductId")] AttributeSet attributeSet)
        {
            if (id != attributeSet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attributeSet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttributeSetExists(attributeSet.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", attributeSet.ProductId);
            return View(attributeSet);
        }

        // GET: AttributeSet/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.AttributeSets == null)
            {
                return NotFound();
            }

            var attributeSet = await _context.AttributeSets
                .Include(a => a.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attributeSet == null)
            {
                return NotFound();
            }

            return View(attributeSet);
        }

        // POST: AttributeSet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.AttributeSets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AttributeSets'  is null.");
            }
            var attributeSet = await _context.AttributeSets.FindAsync(id);
            if (attributeSet != null)
            {
                _context.AttributeSets.Remove(attributeSet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttributeSetExists(string id)
        {
          return (_context.AttributeSets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
