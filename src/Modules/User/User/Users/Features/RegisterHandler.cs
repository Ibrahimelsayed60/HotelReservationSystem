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

    public record RegisterCommand(string FullName, string Email, string Password):ICommand<RegisterResult>;

    public record RegisterResult(bool IsSuccess);

    public class RegisterHandler : ICommandHandler<RegisterCommand, RegisterResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;

        public RegisterHandler(UserManager<AppUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existing = await _userManager.FindByEmailAsync(request.Email);

            if (existing == null)
            {
                return new RegisterResult(false);
            }

            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                UserName = request.Email,
                Email = request.Email,
                IsActive = true
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);

            if (!createResult.Succeeded)
                return new RegisterResult(false);

            await _userManager.AddToRoleAsync(user, "Customer");

            await _mediator.Publish(new UserRegisteredEvent(user.Id, user.Email, user.FullName));

            return new RegisterResult(true);

        }
    }
}
