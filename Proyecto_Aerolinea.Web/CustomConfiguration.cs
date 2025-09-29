using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Proyecto_Aerolinea.Web.Data;
using Proyecto_Aerolinea.Web.Services.Abstract;
using Proyecto_Aerolinea.Web.Services.AirportServices;
using Proyecto_Aerolinea.Web.Services.Implementation;
using SystemStore.Services.AirportServices;

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

            //Services
            AddServices(builder);

            return builder;
        }

        public static void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IFlightService, FlightService>();
            builder.Services.AddScoped<AddAirport>();
            builder.Services.AddScoped<UpdateAirport>();
            builder.Services.AddScoped<GetAirportById>();
            builder.Services.AddScoped<DeleteAirport>();
        }
    }
}
