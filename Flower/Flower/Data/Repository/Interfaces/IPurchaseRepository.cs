using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flower.Models;

namespace Flower.Data.Repository.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<IEnumerable<Purchase>> GetAll();
        Task<Purchase> GetById(string Id);
        void Add(Purchase purchase);
        void Update(Purchase purchase);
        void Delete(Purchase purchase);
    }
}
