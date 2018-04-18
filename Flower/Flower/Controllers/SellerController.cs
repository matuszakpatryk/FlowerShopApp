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
    [Authorize(Roles = "Admin, Employee")]
    public class SellerController : Controller
    {
        private readonly ISellerRepository _context;

        public SellerController(ISellerRepository context)
        {
            _context = context;
        }

        // GET: Seller
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetAll());
        }

        // GET: Seller/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.GetById(id.ToString());
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // GET: Seller/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Seller/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SellerID,Name,Surname,EmployeeNumber,Phone,Country,City")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seller);
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }

        // GET: Seller/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.GetById(id.ToString());
            if (seller == null)
            {
                return NotFound();
            }
            return View(seller);
        }

        // POST: Seller/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SellerID,Name,Surname,EmployeeNumber,Phone,Country,City")] Seller seller)
        {
            if (id != seller.SellerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seller);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var temp = await SellerExists(seller.SellerID);
                    if (!temp)
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
            return View(seller);
        }

        // GET: Seller/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.GetById(id.ToString());
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // POST: Seller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seller = await _context.GetById(id.ToString());
            try
            {
                _context.Delete(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(seller);
            }
        }

        private async Task<bool> SellerExists(int id)
        {
            var seller = await _context.GetById(id.ToString());
            if(seller == null)
            {
                return false;
            }
            return true;
        }
    }
}
