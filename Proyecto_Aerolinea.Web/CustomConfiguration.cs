using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Proyecto_Aerolinea.Web.Data;
using Proyecto_Aerolinea.Web.Services.Abstract;
using Proyecto_Aerolinea.Web.Services.Implementation;

namespace Proyecto_Aerolinea.Web
{
    public static class CustomConfiguration
    {
        public static WebApplicationBuilder AddCustomConfiguration (this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
            });

            //AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));

            //Toast Notification
            builder.Services.AddNotyf(config =>
            {
                config.DurationInSeconds = 10;
                config.IsDismissable = true;
                config.Position = NotyfPosition.BottomRight;
            });

            //Services
            AddServices(builder);

            return builder;
        }
        public static void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IFlightService, FlightService>();
            builder.Services.AddScoped<IAirportService, AirportService>();
            builder.Services.AddScoped<ITicketServices, TicketServices>();
            builder.Services.AddScoped<IAircraftService, AircraftService>();
        }

        public static WebApplication AddCustomWebApplicationConfiguration(this WebApplication app)
        {
            app.UseNotyf();

            return app;
        }
    }
}
