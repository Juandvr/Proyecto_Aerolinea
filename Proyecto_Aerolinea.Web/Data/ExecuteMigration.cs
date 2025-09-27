using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Proyecto_Aerolinea.Web.Data
{
    // Esta clase le dice a EF Core cómo construir el DataContext en tiempo de diseño (migraciones)
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            // Cargar configuración desde appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Tomar la cadena de conexión "MyConnection"
            var connectionString = configuration.GetConnectionString("MyConnection");

            // Configurar el DbContext con esa conexión
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new DataContext(optionsBuilder.Options);
        }
    }
}
