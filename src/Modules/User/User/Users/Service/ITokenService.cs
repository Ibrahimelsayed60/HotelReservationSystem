using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Users.Models;

namespace User.Users.Service
{
    public interface ITokenService
    {

        Task<string> GenerateTokenAsync(AppUser user, UserManager<AppUser> userManager);

    }
}
