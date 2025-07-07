using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Users.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }

        public bool IsActive { get; set; }

    }
}
