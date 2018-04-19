using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flower.Data.Repository.Interfaces;
using Flower.Models;

namespace FlowerShopAppTests.Mocks
{
    public class ProductRepositoryMock : IProductRepository
    {
        IList<Product> products = new List<Product>();

        public void Add(Product product)
        {
            if(products.Count() != 0)
            {
                int temp = products.Count() - 1;
                var lastKey = products[temp].ProductID;
                int nextKey = lastKey;
                nextKey++;
                product.ProductID = nextKey;
            }
            else
            {
                product.ProductID = 1;
            }

            products.Add(product);
        }

        public void Delete(Product product)
        {
            products.Remove(product);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return products;
        }

        public async Task<Product> GetById(string id)
        {
            int temp = Int32.Parse(id);
            temp -= 1;
            var product = products[temp];
            return await Task.FromResult(product);
        }

        public void Update(Product product)
        {
            var id = product.ProductID;
            if (id == 0)
            {
                throw new Exception();
            }
            products.Remove(product);
            products.Add(product);
        }
    }
}
