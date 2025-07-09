using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Users.Features
{
    public record LoginCommand(string Email, string Password):ICommand<LoginResult>;

    public record LoginResult(string Token, string Email, string FullName, List<String> Roles);

    public class LoginHandler : ICommandHandler<LoginCommand, LoginResult>
    {
        public Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
