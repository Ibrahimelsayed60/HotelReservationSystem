using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Users.Models;

namespace User.Data
{
    public class UserDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {

        public UserDbContext(DbContextOptions<UserDbContext> options):base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("auth");

            builder.Entity<AppUser>().ToTable("Users");

            builder.Entity<IdentityRole<Guid>>().ToTable("Roles");

            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");

            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");

            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");

            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");

            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");

        }

    }
}
