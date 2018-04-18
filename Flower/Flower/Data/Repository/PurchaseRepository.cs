using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flower.Data.Repository.Interfaces;
using Flower.Models;
using Microsoft.EntityFrameworkCore;

namespace Flower.Data.Repository
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PurchaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Purchase purchase)
        {
            _dbContext.Add(purchase);
        }

        public void Delete(Purchase purchase)
        {
            _dbContext.Remove(purchase);
        }

        public async Task<IEnumerable<Purchase>> GetAll()
        {
            return await _dbContext.Purchase.ToListAsync();
        }

        public async Task<Purchase> GetById(string Id)
        {
            return await _dbContext.Purchase.FirstOrDefaultAsync(x => x.PurchaseID == Int64.Parse(Id));
        }

        public void Update(Purchase purchase)
        {
            _dbContext.Update(purchase);
        }
    }
}
