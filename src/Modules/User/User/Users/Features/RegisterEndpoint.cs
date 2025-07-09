using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Users.Dtos;

namespace User.Users.Features
{
    public record RegisterRequest(RegisterDto Register);



    public class RegisterEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/register", async (RegisterRequest request, ISender sender) =>
            {
                var command = new RegisterCommand(request.Register.FullName, request.Register.Email, request.Register.Password);

                var result = await sender.Send(command);

                return Results.Ok(result);

            })
                .WithName("Register")
                .Produces<RegisterResult>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Register user")
                .WithDescription("Register User")
                ;
        }
    }
}
