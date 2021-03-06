
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Boutique.Data;
using Boutique.Models;
using Boutique.Views.Shop.SessionHelper;

namespace Boutique.Controllers
{
    public class ShopController : Controller
    {
        private readonly BoutiqueContext _context;

        public ShopController(BoutiqueContext context)
        {
            _context = context;
        }

        // GET: Shop
        public async Task<IActionResult> Index()
        {
            if (SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "products") != null)

            {
                List<Product> products = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "products");
                ViewBag.cartCount = products.Count();
            }
            else
            {
                ViewBag.cartCount = 0;
            }
            return View(await _context.Product.ToListAsync());
        }

        // GET: Shop/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Shop/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }

        // POST: Shop/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,ProductType,ProductPrice,ProductImg,QuantiteDispo")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }


        public async Task<IActionResult> ajoutPanier(int? id)
        {

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }


            List<Product> products;
            if (SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "products") != null)
            {
                products = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "products");
                foreach (var p in products)
                {
                    if (p.ProductId == id)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            else
            {
                products = new List<Product>();
            }


            products.Add(product);




            SessionHelper.SetObjectAsJson(HttpContext.Session, "products", products);
            return RedirectToAction("Index");

        }


        // GET: Shop/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Shop/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,ProductType,ProductPrice,ProductImg,QuantiteDispo")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            return View(product);
        }

        // GET: Shop/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Shop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
        public async Task<IActionResult> Cart()
        {
            return View();
        }
    }
}
