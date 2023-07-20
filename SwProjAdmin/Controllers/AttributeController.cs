using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SwProjAdmin.Data;
using SwProjAdmin.Models;
using Attribute = SwProjAdmin.Models.Attribute;

namespace SwProjAdmin.Controllers
{
    public class AttributeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttributeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Attribute
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Attributes.Include(a => a.AttributeSet);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Attribute/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Attributes == null)
            {
                return NotFound();
            }

            var attribute = await _context.Attributes
                .Include(a => a.AttributeSet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attribute == null)
            {
                return NotFound();
            }

            return View(attribute);
        }

        // GET: Attribute/Create
        public IActionResult Create()
        {
            ViewData["AttributeSetId"] = new SelectList(_context.AttributeSets, "Id", "Id");
            return View();
        }

        // POST: Attribute/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,DisplayValue,AttributeSetId")] Attribute attribute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attribute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AttributeSetId"] = new SelectList(_context.AttributeSets, "Id", "Id", attribute.AttributeSetId);
            return View(attribute);
        }

        // GET: Attribute/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Attributes == null)
            {
                return NotFound();
            }

            var attribute = await _context.Attributes.FindAsync(id);
            if (attribute == null)
            {
                return NotFound();
            }
            ViewData["AttributeSetId"] = new SelectList(_context.AttributeSets, "Id", "Id", attribute.AttributeSetId);
            return View(attribute);
        }

        // POST: Attribute/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value,DisplayValue,AttributeSetId")] Attribute attribute)
        {
            if (id != attribute.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attribute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttributeExists(attribute.Id))
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
            ViewData["AttributeSetId"] = new SelectList(_context.AttributeSets, "Id", "Id", attribute.AttributeSetId);
            return View(attribute);
        }

        // GET: Attribute/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Attributes == null)
            {
                return NotFound();
            }

            var attribute = await _context.Attributes
                .Include(a => a.AttributeSet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attribute == null)
            {
                return NotFound();
            }

            return View(attribute);
        }

        // POST: Attribute/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Attributes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Attributes'  is null.");
            }
            var attribute = await _context.Attributes.FindAsync(id);
            if (attribute != null)
            {
                _context.Attributes.Remove(attribute);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttributeExists(int id)
        {
          return (_context.Attributes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
