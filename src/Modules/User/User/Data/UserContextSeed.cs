using Microsoft.AspNetCore.Identity;
using Shared.Data.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Users.Models;

namespace User.Data
{
    public class UserContextSeed : IDataSeeder
    {
        private readonly UserDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public UserContextSeed(UserDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAllAsync()
        {
            string[] roles = { "Admin", "Customer", "Staff" };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }
    }
}
