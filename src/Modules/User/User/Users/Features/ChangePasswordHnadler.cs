using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Users.Features
{
    public record ChangePasswordCommand(Guid UserId, string CurrentPassword, string NewPassword) :ICommand<ChangePasswordResult>;

    public record ChangePasswordResult(bool IsSuccess);

    public class ChangePasswordHnadler : ICommandHandler<ChangePasswordCommand, ChangePasswordResult>
    {
        public Task<ChangePasswordResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
