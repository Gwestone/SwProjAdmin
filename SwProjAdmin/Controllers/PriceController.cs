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
    public class PriceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PriceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Price
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Prices.Include(p => p.Currency).Include(p => p.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Price/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prices == null)
            {
                return NotFound();
            }

            var price = await _context.Prices
                .Include(p => p.Currency)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // GET: Price/Create
        public IActionResult Create()
        {
            ViewData["CurrencyId"] = new SelectList(_context.Currencies, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: Price/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,CurrencyId,ProductId")] Price price)
        {
            var currency = _context.Currencies.FirstOrDefault(c => c.Id == price.CurrencyId); // Retrieve the Category entity from the database
            var product = _context.Products.Include(p => p.Category).FirstOrDefault(c => c.Id == price.ProductId); // Retrieve the Category entity from the database
            price.Currency = currency;
            price.Product = product;
            currency.Prices.Add(price);
            product.Prices.Add(price);
            
            ModelState.Clear();
            TryValidateModel(price);
            
            Console.WriteLine(ModelState.IsValid);
            if (ModelState.IsValid)
            {
                _context.Add(price);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // You can access individual errors and error messages like this:
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        var errorMessage = error.ErrorMessage;
                        // Do something with the error message, like logging or displaying to the user.
                    }
                }
            }
            ViewData["CurrencyId"] = new SelectList(_context.Currencies, "Id", "Id", price.CurrencyId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", price.ProductId);
            return View(price);
        }

        // GET: Price/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prices == null)
            {
                return NotFound();
            }

            var price = await _context.Prices.FindAsync(id);
            if (price == null)
            {
                return NotFound();
            }
            ViewData["CurrencyId"] = new SelectList(_context.Currencies, "Id", "Id", price.CurrencyId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", price.ProductId);
            return View(price);
        }

        // POST: Price/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,CurrencyId,ProductId")] Price price)
        {
            if (id != price.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(price);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriceExists(price.Id))
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
            ViewData["CurrencyId"] = new SelectList(_context.Currencies, "Id", "Id", price.CurrencyId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", price.ProductId);
            return View(price);
        }

        // GET: Price/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prices == null)
            {
                return NotFound();
            }

            var price = await _context.Prices
                .Include(p => p.Currency)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // POST: Price/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prices == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Prices'  is null.");
            }
            var price = await _context.Prices.FindAsync(id);
            if (price != null)
            {
                _context.Prices.Remove(price);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriceExists(int id)
        {
          return (_context.Prices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
