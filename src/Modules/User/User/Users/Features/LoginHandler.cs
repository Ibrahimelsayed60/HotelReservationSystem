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
using User.Users.Service;

namespace User.Users.Features
{
    public record LoginCommand(string Email, string Password):ICommand<LoginResult>;

    public record LoginResult(string Token, string Email, string FullName, List<string> Roles);

    public class LoginHandler : ICommandHandler<LoginCommand, LoginResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMediator _mediator;

        public LoginHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMediator mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mediator = mediator;
        }

        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !user.IsActive)
                throw new Exception("Invalid credientials");

            var validPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!validPassword)
                throw new Exception("Invalid credientials");

            var roles = (await _userManager.GetRolesAsync(user)).ToList();
            var token = await _tokenService.GenerateTokenAsync(user, _userManager);

            await _mediator.Publish(new UserLoggedInEvent(user.Id, user.Email));


            return new LoginResult
            (
                token,
                user.Email,
                user.FullName,
                roles
            );

        }
    }
}
