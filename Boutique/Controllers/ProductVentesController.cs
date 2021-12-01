using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Boutique.Data;
using Boutique.Models;

namespace Boutique.Controllers
{
    public class ProductVentesController : Controller
    {
        private readonly BoutiqueContext _context;

        public ProductVentesController(BoutiqueContext context)
        {
            _context = context;
        }

        // GET: ProductVentes
        public async Task<IActionResult> Index()
        {
            var boutiqueContext = _context.ProductVente.Include(p => p.Product).Include(p => p.Vente);
            return View(await boutiqueContext.ToListAsync());
        }

        // GET: ProductVentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productVente = await _context.ProductVente
                .Include(p => p.Product)
                .Include(p => p.Vente)
                .FirstOrDefaultAsync(m => m.ProductVenteId == id);
            if (productVente == null)
            {
                return NotFound();
            }

            return View(productVente);
        }

        // GET: ProductVentes/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name");
            ViewData["VenteId"] = new SelectList(_context.Set<Vente>(), "VenteId", "VenteId");
            return View();
        }

        // POST: ProductVentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductVenteId,ProductId,VenteId,QuantiteDesi")] ProductVente productVente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productVente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", productVente.ProductId);
            ViewData["VenteId"] = new SelectList(_context.Set<Vente>(), "VenteId", "VenteId", productVente.VenteId);
            return View(productVente);
        }

        // GET: ProductVentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productVente = await _context.ProductVente.FindAsync(id);
            if (productVente == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", productVente.ProductId);
            ViewData["VenteId"] = new SelectList(_context.Set<Vente>(), "VenteId", "VenteId", productVente.VenteId);
            return View(productVente);
        }

        // POST: ProductVentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductVenteId,ProductId,VenteId,QuantiteDesi")] ProductVente productVente)
        {
            if (id != productVente.ProductVenteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productVente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductVenteExists(productVente.ProductVenteId))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", productVente.ProductId);
            ViewData["VenteId"] = new SelectList(_context.Set<Vente>(), "VenteId", "VenteId", productVente.VenteId);
            return View(productVente);
        }

        // GET: ProductVentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productVente = await _context.ProductVente
                .Include(p => p.Product)
                .Include(p => p.Vente)
                .FirstOrDefaultAsync(m => m.ProductVenteId == id);
            if (productVente == null)
            {
                return NotFound();
            }

            return View(productVente);
        }

        // POST: ProductVentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productVente = await _context.ProductVente.FindAsync(id);
            _context.ProductVente.Remove(productVente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductVenteExists(int id)
        {
            return _context.ProductVente.Any(e => e.ProductVenteId == id);
        }
    }
}
