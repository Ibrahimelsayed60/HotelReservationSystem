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

    public record DeactivateUserCommand(Guid UserId):ICommand<DeactivateUserResult>;

    public record DeactivateUserResult(bool IsSuccess);

    public class DeactivateUserHandler : ICommandHandler<DeactivateUserCommand, DeactivateUserResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;

        public DeactivateUserHandler(UserManager<AppUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<DeactivateUserResult> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null || !user.IsActive)
                throw new Exception("User not found or already deactivated.");

            user.IsActive = false;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new Exception("A problem happened");

            await _mediator.Publish(new UserDeactivatedEvent(user.Id, user.Email));
            return new DeactivateUserResult(true);
        }
    }
}
