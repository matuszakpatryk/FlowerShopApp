using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flower.Models;
using Microsoft.EntityFrameworkCore;

namespace Flower.Data.Repository.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<IEnumerable<Purchase>> GetAll();
        Task<Purchase> GetById(string Id);
        void Add(Purchase purchase);
        void Update(Purchase purchase);
        void Delete(Purchase purchase);

        DbSet<Flower.Models.Customer> Customer { get; set; }
        DbSet<Flower.Models.Seller> Seller { get; set; }
        DbSet<Flower.Models.Product> Product { get; set; }
        DbSet<Flower.Models.Purchase> Purchase { get; set; }
    }   
}
