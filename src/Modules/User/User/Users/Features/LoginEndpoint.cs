using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Users.Dtos;

namespace User.Users.Features
{

    public record LoginRequest(LoginDto Login);

    public class LoginEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/login", async (LoginRequest request, ISender sender) =>
            {
                var command = new LoginCommand(request.Login.Email, request.Login.Password);

                var result = await sender.Send(command);

                return Results.Ok(result);

            })
                .WithName("Login")
                .Produces<LoginResult>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Login user")
                .WithDescription("Login User")
                ;
        }
    }
}
