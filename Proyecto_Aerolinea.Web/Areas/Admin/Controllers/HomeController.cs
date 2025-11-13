using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Aerolinea.Web.Data;

namespace Proyecto_Aerolinea.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Contar los registros - versión simple
            var totalVuelos = _context.Flights.Count();
            var totalAeropuertos = _context.Airports.Count();
            var totalAviones = _context.Aircrafts.Count();
            var reservasHoy = 0;

            // Crear el modelo
            var model = new
            {
                TotalVuelos = totalVuelos,
                TotalAeropuertos = totalAeropuertos,
                TotalAviones = totalAviones,
                ReservasHoy = reservasHoy
            };

            return View(model);
        }
    }
}
