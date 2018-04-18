using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flower.Data.Repository.Interfaces;
using Flower.Models;
using Microsoft.EntityFrameworkCore;

namespace Flower.Data.Repository
{
    public class SellerRepository : ISellerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SellerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Seller seller)
        {
            _dbContext.Add(seller);
        }

        public void Delete(Seller seller)
        {
            _dbContext.Remove(seller);
        }

        public async Task<IEnumerable<Seller>> GetAll()
        {
            return await _dbContext.Seller.ToListAsync();
        }

        public async Task<Seller> GetById(string Id)
        {
            return await _dbContext.Seller.FirstOrDefaultAsync(x => x.SellerID == Int64.Parse(Id));
        }

        public void Update(Seller seller)
        {
            _dbContext.Update(seller);
        }
    }
}
