using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Core.Attributes;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Services.Abstract;
using System.Threading.Tasks;

namespace Proyecto_Aerolinea.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AirportsController : Controller
    {
        private readonly IAirportService _airportService;
        private readonly INotyfService _notyfservice;

        public AirportsController(IAirportService airportService, INotyfService notyfservice)
        {
            _airportService = airportService;
            _notyfservice = notyfservice;
        }

        public async Task<IActionResult> Index()
        {
            Response<List<AirportDTO>> response = await _airportService.GetListAsync();

            if (!response.Succeed)
            {
                _notyfservice.Error(response.Message);
                return RedirectToAction("Index", "Home");
            }

            return View(response.Result);
        }

        // Show

        [HttpGet]
        [CustomAuthorize(permission: "showAirPlanes", module: "Aeropuertos")]
        public IActionResult Create()
        {
            return View();
        }

        // Create

        [HttpPost]
        [CustomAuthorize(permission: "createAirPlanes", module: "Aeropuertos")]
        public async Task<IActionResult> Create([FromForm] AirportDTO dto)
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

        [HttpGet]
        [CustomAuthorize(permission: "createAirPlanes", module: "Aeropuertos")]
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            Response<AirportDTO> response = await _airportService.GetOneAsync(id);

            if (!response.Succeed)
            {
                _notyfservice.Error(response.Message);
                return RedirectToAction(nameof(Index));
            }

            return View(response.Result);
        }

        // Edit

        [HttpPost]
        [CustomAuthorize(permission: "updateAirPlanes", module: "Aeropuertos")]
        public async Task<IActionResult> Edit([FromForm] AirportDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notyfservice.Error("Debe ajustar los errores de validación");
                return View(dto);
            }

            Response<AirportDTO> response = await _airportService.UpdateAsync(dto);

            if (!response.Succeed)
            {
                _notyfservice.Error(response.Message);
                return View(dto);
            }

            _notyfservice.Success(response.Message);
            return RedirectToAction(nameof(Index));
        }

        // Delete

        [HttpPost]
        [CustomAuthorize(permission: "deleteAirPlanes", module: "Aeropuertos")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Response<object> response = await _airportService.DeleteAsync(id);

            if (!response.Succeed)
            {
                _notyfservice.Error(response.Message);
            }
            else
            {
                _notyfservice.Success(response.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
