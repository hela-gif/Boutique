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
    public class FournisseursController : Controller
    {
        private readonly BoutiqueContext _context;

        public FournisseursController(BoutiqueContext context)
        {
            _context = context;
        }

        // GET: Fournisseurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fournisseur.ToListAsync());
        }

        // GET: Fournisseurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fournisseur = await _context.Fournisseur
                .FirstOrDefaultAsync(m => m.FournisseurId == id);
            if (fournisseur == null)
            {
                return NotFound();
            }

            return View(fournisseur);
        }

        // GET: Fournisseurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fournisseurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FournisseurId,Nom,Prenom,Mail,Addresse,Telephone,Age")] Fournisseur fournisseur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fournisseur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fournisseur);
        }

        // GET: Fournisseurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fournisseur = await _context.Fournisseur.FindAsync(id);
            if (fournisseur == null)
            {
                return NotFound();
            }
            return View(fournisseur);
        }

        // POST: Fournisseurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FournisseurId,Nom,Prenom,Mail,Addresse,Telephone,Age")] Fournisseur fournisseur)
        {
            if (id != fournisseur.FournisseurId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fournisseur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FournisseurExists(fournisseur.FournisseurId))
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
            return View(fournisseur);
        }

        // GET: Fournisseurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fournisseur = await _context.Fournisseur
                .FirstOrDefaultAsync(m => m.FournisseurId == id);
            if (fournisseur == null)
            {
                return NotFound();
            }

            return View(fournisseur);
        }

        // POST: Fournisseurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fournisseur = await _context.Fournisseur.FindAsync(id);
            _context.Fournisseur.Remove(fournisseur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FournisseurExists(int id)
        {
            return _context.Fournisseur.Any(e => e.FournisseurId == id);
        }
    }
}
