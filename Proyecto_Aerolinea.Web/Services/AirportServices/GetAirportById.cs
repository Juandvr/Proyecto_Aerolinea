using Microsoft.EntityFrameworkCore;
using Proyecto_Aerolinea.Web.Data;
using Proyecto_Aerolinea.Web.Models.Entities;

namespace SystemStore.Services.AirportServices
{
    public class GetAirportById
    {
        private readonly DataContext _context;

        public GetAirportById(DataContext context)
        {
            _context = context;
        }
        public async Task<Airport?> Execute(Guid id)
        {
            return await _context.Airports.FirstOrDefaultAsync(p => p.AirportId == id);
        }

    }
}

