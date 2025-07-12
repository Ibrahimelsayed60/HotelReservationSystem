using Carter;
using Serilog;
using Shared.Exceptions.Handler;
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

            var roomAssembly = typeof(RoomModule).Assembly;
            var userAssembly = typeof(UserModule).Assembly;
            var reservationAssembly = typeof(ReservationModule).Assembly;


            builder.Services
                .AddCarterWithAssemblies(roomAssembly, userAssembly, reservationAssembly);

            builder.Services
                .AddMediatRWithAssemblies(roomAssembly, userAssembly, reservationAssembly);

            builder.Services
                .AddRoomModule(builder.Configuration)
                .AddUserModule(builder.Configuration)
                .AddReservationModule(builder.Configuration)
                .AddReportingModule(builder.Configuration)
                .AddPaymentModule(builder.Configuration)
                .AddOfferModule(builder.Configuration)
                .AddNotificationModule(builder.Configuration)
                .AddFeedbackModule(builder.Configuration);

            builder.Services
                .AddExceptionHandler<CustomExceptionHandler>();

            builder.Services.AddAuthorization();

            var app = builder.Build();

            app.MapCarter();

            app.UseSerilogRequestLogging();

            app.UseExceptionHandler(options => { });

            app.UseAuthentication();
            app.UseAuthorization();

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
