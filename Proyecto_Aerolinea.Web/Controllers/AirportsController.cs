using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Core.Attributes;
using Proyecto_Aerolinea.Web.Core.Pagination;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Services.Abstract;
using System.Threading.Tasks;

namespace Proyecto_Aerolinea.Web.Controllers
{
    public class AirportsController : Controller
    {
        private readonly IAirportService _airportService;
        private readonly INotyfService _notyfservice;

        public AirportsController(IAirportService airportService, INotyfService notyfservice)
        {
            _airportService = airportService;
            _notyfservice = notyfservice;
        }

        // Create 

        [HttpPost]
        [CustomAuthorize(permission: "createAirPlanes", module: "Aeropuertos")]
        public async Task<IActionResult> Create(AirportDTO dto)
        {
            if (!ModelState.IsValid) 
            {
                _notyfservice.Error("Debe ajustar los errores de validación");
                return View(dto);
            }

            Response<AirportDTO> response = await _airportService.CreateAsync(dto);

            if (!response.Succeed)
            {
                _notyfservice.Error(response.Message);
                return View(dto);
            }

            _notyfservice.Success(response.Message);    
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var request = new PaginationRequest
            {
                Page = pageNumber,
                RecordsPerPage = pageSize
            };

            Response<PaginationResponse<AirportDTO>> response =
                await _airportService.GetPaginatedListAsync(request);

            if (!response.Succeed)
            {
                _notyfservice.Error(response.Message);
                return RedirectToAction("Index", "Home");
            }

            return View(response.Result);
        }
    }
}
