using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Flower.Models;

namespace Flower.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Flower.Models.Customer> Customer { get; set; }

        public DbSet<Flower.Models.Seller> Seller { get; set; }

        public DbSet<Flower.Models.Product> Product { get; set; }

        public DbSet<Flower.Models.Purchase> Purchase { get; set; }
    }
}
