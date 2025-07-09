using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Users.Events;
using User.Users.Models;

namespace User.Users.Features
{
    public record ChangePasswordCommand(Guid UserId, string CurrentPassword, string NewPassword) :ICommand<ChangePasswordResult>;

    public record ChangePasswordResult(bool IsSuccess);

    public class ChangePasswordHnadler : ICommandHandler<ChangePasswordCommand, ChangePasswordResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;

        public ChangePasswordHnadler(UserManager<AppUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<ChangePasswordResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null || !user.IsActive)
                throw new Exception("User not found.");

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
                throw new Exception("An error happened during change Password");

            await _mediator.Publish(new PasswordChangedEvent(user.Id, user.Email));

            return new ChangePasswordResult(true);
        }
    }
}
