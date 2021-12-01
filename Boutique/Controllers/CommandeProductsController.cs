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
    public class CommandeProductsController : Controller
    {
        private readonly BoutiqueContext _context;

        public CommandeProductsController(BoutiqueContext context)
        {
            _context = context;
        }

        // GET: CommandeProducts
        public async Task<IActionResult> Index()
        {
            var boutiqueContext = _context.CommandeProduct.Include(c => c.Commande).Include(c => c.Product);
            return View(await boutiqueContext.ToListAsync());
        }

        // GET: CommandeProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commandeProduct = await _context.CommandeProduct
                .Include(c => c.Commande)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.CommandeProductId == id);
            if (commandeProduct == null)
            {
                return NotFound();
            }

            return View(commandeProduct);
        }

        // GET: CommandeProducts/Create
        public IActionResult Create()
        {
            ViewData["CommandeId"] = new SelectList(_context.Commande, "CommandeId", "CommandeId");
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Name");
            return View();
        }

        // POST: CommandeProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommandeProductId,ProductId,CommandeId,Quantite")] CommandeProduct commandeProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commandeProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CommandeId"] = new SelectList(_context.Commande, "CommandeId", "CommandeId", commandeProduct.CommandeId);
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Name", commandeProduct.ProductId);
            return View(commandeProduct);
        }

        // GET: CommandeProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commandeProduct = await _context.CommandeProduct.FindAsync(id);
            if (commandeProduct == null)
            {
                return NotFound();
            }
            ViewData["CommandeId"] = new SelectList(_context.Commande, "CommandeId", "CommandeId", commandeProduct.CommandeId);
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Name", commandeProduct.ProductId);
            return View(commandeProduct);
        }

        // POST: CommandeProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommandeProductId,ProductId,CommandeId,Quantite")] CommandeProduct commandeProduct)
        {
            if (id != commandeProduct.CommandeProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commandeProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommandeProductExists(commandeProduct.CommandeProductId))
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
            ViewData["CommandeId"] = new SelectList(_context.Commande, "CommandeId", "CommandeId", commandeProduct.CommandeId);
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Name", commandeProduct.ProductId);
            return View(commandeProduct);
        }

        // GET: CommandeProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commandeProduct = await _context.CommandeProduct
                .Include(c => c.Commande)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.CommandeProductId == id);
            if (commandeProduct == null)
            {
                return NotFound();
            }

            return View(commandeProduct);
        }

        // POST: CommandeProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commandeProduct = await _context.CommandeProduct.FindAsync(id);
            _context.CommandeProduct.Remove(commandeProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommandeProductExists(int id)
        {
            return _context.CommandeProduct.Any(e => e.CommandeProductId == id);
        }
    }
}
