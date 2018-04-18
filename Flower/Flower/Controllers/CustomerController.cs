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
    [Authorize(Roles = "Admin, Employee, User")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _context;

        public CustomerController(ICustomerRepository context)
        {
            _context = context;
        }

        // GET: Customer
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var customers = await _context.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.Name.ToUpper().Contains(searchString.ToUpper())
                                            || c.Surname.ToUpper().Contains(searchString.ToUpper()) || c.Email.ToUpper().Contains(searchString.ToUpper())
                                            || c.Phone.ToUpper().Contains(searchString.ToUpper()) || c.Country.ToUpper().Contains(searchString.ToUpper())
                                            || c.City.ToUpper().Contains(searchString.ToUpper()));
            }
            return View(customers);
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.GetById(id.ToString());
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,Name,Surname,Phone,Email,Country,City")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.GetById(id.ToString());
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerID,Name,Surname,Phone,Email,Country,City")] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool temp = await CustomerExists(customer.CustomerID);
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
            return View(customer);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.GetById(id.ToString());
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.GetById(id.ToString());
            try
            {
                _context.Delete(customer);
                return RedirectToAction(nameof(Index));
            } catch (Exception e)
            {
                return View(customer);
            }
            
        }

        private async Task<bool> CustomerExists(int id)
        {
            var customer = await _context.GetById(id.ToString());
            if(customer == null)
            {
                return false;
            }
            return true;
        }
    }
}
