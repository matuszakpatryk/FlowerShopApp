using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Flower.Data;
using Flower.Models;
using Microsoft.AspNetCore.Authorization;

namespace Flower.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class PurchaseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Purchase
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Purchase.Include(p => p.Customer).Include(p => p.Product).Include(p => p.Seller);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Purchase/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase
                .Include(p => p.Customer)
                .Include(p => p.Product)
                .Include(p => p.Seller)
                .SingleOrDefaultAsync(m => m.PurchaseID == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // GET: Purchase/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "City");
            ViewData["ProductID"] = new SelectList(_context.Product, "ProductID", "Name");
            ViewData["SellerID"] = new SelectList(_context.Seller, "SellerID", "City");
            return View();
        }

        // POST: Purchase/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseID,SellerID,CustomerID,ProductID,PurchaseDate")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "City", purchase.CustomerID);
            ViewData["ProductID"] = new SelectList(_context.Product, "ProductID", "Name", purchase.ProductID);
            ViewData["SellerID"] = new SelectList(_context.Seller, "SellerID", "City", purchase.SellerID);
            return View(purchase);
        }

        // GET: Purchase/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase.SingleOrDefaultAsync(m => m.PurchaseID == id);
            if (purchase == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "City", purchase.CustomerID);
            ViewData["ProductID"] = new SelectList(_context.Product, "ProductID", "Name", purchase.ProductID);
            ViewData["SellerID"] = new SelectList(_context.Seller, "SellerID", "City", purchase.SellerID);
            return View(purchase);
        }

        // POST: Purchase/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseID,SellerID,CustomerID,ProductID,PurchaseDate")] Purchase purchase)
        {
            if (id != purchase.PurchaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.PurchaseID))
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
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "City", purchase.CustomerID);
            ViewData["ProductID"] = new SelectList(_context.Product, "ProductID", "Name", purchase.ProductID);
            ViewData["SellerID"] = new SelectList(_context.Seller, "SellerID", "City", purchase.SellerID);
            return View(purchase);
        }

        // GET: Purchase/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase
                .Include(p => p.Customer)
                .Include(p => p.Product)
                .Include(p => p.Seller)
                .SingleOrDefaultAsync(m => m.PurchaseID == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // POST: Purchase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchase = await _context.Purchase.SingleOrDefaultAsync(m => m.PurchaseID == id);
            _context.Purchase.Remove(purchase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseExists(int id)
        {
            return _context.Purchase.Any(e => e.PurchaseID == id);
        }
    }
}
