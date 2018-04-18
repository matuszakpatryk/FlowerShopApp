using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flower.Data.Repository.Interfaces;
using Flower.Models;
using Microsoft.EntityFrameworkCore;

namespace Flower.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Customer customer)
        {
            _dbContext.Add(customer);
        }

        public void Delete(Customer customer)
        {
            _dbContext.Remove(customer);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _dbContext.Customer.ToListAsync();
        }

        public async Task<Customer> GetById(string Id)
        {
            return await _dbContext.Customer.FirstOrDefaultAsync(x => x.CustomerID == Int64.Parse(Id));
        }

        public void Update(Customer customer)
        {
            _dbContext.Update(customer);
        }
    }
}
