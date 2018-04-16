using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flower.Data.Repository.Interfaces;
using Flower.Models;
using Microsoft.EntityFrameworkCore;

namespace Flower.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Product product)
        {
            _dbContext.Add(product);
        }

        public void Delete(Product product)
        {
            _dbContext.Remove(product);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _dbContext.Product.ToListAsync();
        }

        public async Task<Product> GetById(string Id)
        {
            return await _dbContext.Product.FirstOrDefaultAsync(x => x.ProductID == Int64.Parse(Id));
        }

        public void Update(Product product)
        {
            _dbContext.Update(product);
        }
    }
}
