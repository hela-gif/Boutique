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
    public class LivraisonsController : Controller
    {
        private readonly BoutiqueContext _context;

        public LivraisonsController(BoutiqueContext context)
        {
            _context = context;
        }

        // GET: Livraisons
        public async Task<IActionResult> Index()
        {
            return View(await _context.Livraison.ToListAsync());
        }

        // GET: Livraisons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livraison = await _context.Livraison
                .FirstOrDefaultAsync(m => m.LivraisonId == id);
            if (livraison == null)
            {
                return NotFound();
            }

            return View(livraison);
        }

        // GET: Livraisons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Livraisons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LivraisonId,adresseLivraison")] Livraison livraison)
        {
            if (ModelState.IsValid)
            {
                _context.Add(livraison);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(livraison);
        }

        // GET: Livraisons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livraison = await _context.Livraison.FindAsync(id);
            if (livraison == null)
            {
                return NotFound();
            }
            return View(livraison);
        }

        // POST: Livraisons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LivraisonId,adresseLivraison")] Livraison livraison)
        {
            if (id != livraison.LivraisonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livraison);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivraisonExists(livraison.LivraisonId))
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
            return View(livraison);
        }

        // GET: Livraisons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livraison = await _context.Livraison
                .FirstOrDefaultAsync(m => m.LivraisonId == id);
            if (livraison == null)
            {
                return NotFound();
            }

            return View(livraison);
        }

        // POST: Livraisons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livraison = await _context.Livraison.FindAsync(id);
            _context.Livraison.Remove(livraison);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivraisonExists(int id)
        {
            return _context.Livraison.Any(e => e.LivraisonId == id);
        }
    }
}
