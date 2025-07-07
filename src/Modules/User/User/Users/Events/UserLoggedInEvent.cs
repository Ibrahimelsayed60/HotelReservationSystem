using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Users.Events
{
    public record UserLoggedInEvent(Guid UserId, string Email) :IDomainEvent;
    
}
