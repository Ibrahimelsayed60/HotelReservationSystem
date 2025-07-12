using Carter;
using Serilog;
using Shared.Extensions;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services
            //    .AddCarterWithAssemblies(catalogAssembly, basketAssembly, orderingAssembly);

            builder.Services
                .AddCarterWithAssemblies(typeof(RoomModule).Assembly);

            builder.Services
                .AddRoomModule(builder.Configuration)
                .AddUserModule(builder.Configuration)
                .AddReservationModule(builder.Configuration)
                .AddReportingModule(builder.Configuration)
                .AddPaymentModule(builder.Configuration)
                .AddOfferModule(builder.Configuration)
                .AddNotificationModule(builder.Configuration)
                .AddFeedbackModule(builder.Configuration);

            var app = builder.Build();

            app.MapCarter();

            app.UseSerilogRequestLogging();

            app.UseRoomModule()
                .UseUserModule()
                .UseReservationModule()
                .UseReportingModule()
                .UsePaymentModule()
                .UseOfferModule()
                .UseNotificationModule()
                .UseFeedbackModule();

            //app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
