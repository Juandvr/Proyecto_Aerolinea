using Microsoft.EntityFrameworkCore;
using Proyecto_Aerolinea.Web.Data;
using Proyecto_Aerolinea.Web.Models.Entities;

namespace Proyecto_Aerolinea.Web.Services.AirportServices
{
    public class DeleteAirport
    {
        private readonly DataContext _context;

        public DeleteAirport(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Execute(Guid id)
        {
            var airport = await _context.Airports.FirstOrDefaultAsync(a => a.AirportId == id);
            if (airport == null) return false;

            _context.Airports.Remove(airport);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}