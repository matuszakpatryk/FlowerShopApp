using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flower.Models;

namespace Flower.Data.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(string Id);
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
    }
}
