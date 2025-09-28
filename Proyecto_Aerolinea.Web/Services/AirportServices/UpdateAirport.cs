using Microsoft.EntityFrameworkCore;
using Proyecto_Aerolinea.Web.Data;
using Proyecto_Aerolinea.Web.Models.Entities;

namespace SystemStore.Services.AirportServices
{
    public class UpdateAirport
    {
        private readonly DataContext _context;

        public UpdateAirport(DataContext context)
        {
            _context = context;
        }

        public async Task<Airport> Execute(int id, AirportDto dto)
        {
            var airport = await _context.Airports.FirstOrDefaultAsync(p => p.AirportId == id);
            if (airport == null) return null;
            airport.AirportName = dto.AirportName;
            airport.AirportCity = dto.AirportCity;
            airport.AirportCountry = dto.AirportCountry;
            airport.IATACode = dto.IATACode;

            await _context.SaveChangesAsync();

            return airport;

        }
    }
}
