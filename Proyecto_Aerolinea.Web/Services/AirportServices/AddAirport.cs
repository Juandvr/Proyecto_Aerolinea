using System.Threading.Tasks;
using Proyecto_Aerolinea.Web.Data;
using Proyecto_Aerolinea.Web.Models.Entities;

namespace Proyecto_Aerolinea.Web.Services.AirportServices
{
    public class AddAirport
    {
        private readonly DataContext _context;

        public AddAirport(DataContext context)
        {
            _context = context;
        }
        public async Task<Airport> Execute(AirportDto dto)
        {
            var airport = new Airport
            {
                AirportName = dto.AirportName,
                AirportCity = dto.AirportCity,
                AirportCountry = dto.AirportCountry,
                IATACode = dto.IATACode
            };

            _context.Airports.Add(airport);
            await _context.SaveChangesAsync();

            return airport;
        }
    }
}
