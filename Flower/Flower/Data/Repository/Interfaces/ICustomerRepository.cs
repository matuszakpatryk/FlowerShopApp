using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flower.Models;

namespace Flower.Data.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetById(string Id);
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
    }
}
