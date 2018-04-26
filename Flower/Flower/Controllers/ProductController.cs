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
using Flower.Data.Repository.Interfaces;

namespace Flower.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        private readonly IProductRepository _context;

        public ProductController(IProductRepository context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetAll());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.GetById(id.ToString());
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,Name,Price,Quantity")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.GetById(id.ToString());
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,Name,Price,Quantity")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                }
                catch (Exception)
                {
                    bool temp = await ProductExists(product.ProductID);
                    if (!temp)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.GetById(id.ToString());
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.GetById(id.ToString());
            try
            {
                _context.Delete(product);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(product);
            }
        }

        private async Task<bool> ProductExists(int id)
        {
            var product = await _context.GetById(id.ToString());
            if (product == null)
            {
                return false;
            }
            return true;
        }
    }
}
