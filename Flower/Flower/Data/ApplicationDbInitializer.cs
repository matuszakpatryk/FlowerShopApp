using Flower.Extensions.Helpers;
using Flower.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flower.Data
{
    public class ApplicationDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationDbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void Seed()
        {
            // create database + apply migrations
            _context.Database.Migrate();

            // add example roles
            if (!_context.Roles.Any())
            {
                var roleNames = new[]
                {
                    RoleHelper.Admin,
                    RoleHelper.Employee,
                    RoleHelper.User
                };

                foreach (var roleName in roleNames)
                {
                    var role = new IdentityRole(roleName) { NormalizedName = RoleHelper.Normalize(roleName) };
                    _context.Roles.Add(role);
                }
            }

            _context.SaveChanges();
        }
    }
}
