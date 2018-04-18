using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flower.Models;

namespace Flower.Data.Repository.Interfaces
{
    public interface ISellerRepository
    {
        Task<IEnumerable<Seller>> GetAll();
        Task<Seller> GetById(string Id);
        void Add(Seller seller);
        void Update(Seller seller);
        void Delete(Seller seller);
    }
}
