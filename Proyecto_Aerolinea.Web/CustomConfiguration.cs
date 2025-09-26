using Microsoft.EntityFrameworkCore;
using Proyecto_Aerolinea.Web.Data;

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
            return builder;
        }
    }
}
