using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Users.Events
{
    public record UserRegisteredEvent(Guid UserId, string Email, string FullName) :IDomainEvent;
    
}
