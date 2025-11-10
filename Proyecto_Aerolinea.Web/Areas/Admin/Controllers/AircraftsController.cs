using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Services.Abstract;

namespace Proyecto_Aerolinea.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AircraftsController : Controller
    {
        private readonly IAircraftService _AircraftService;
        private readonly INotyfService _notyfservice;

        public AircraftsController(IAircraftService AircraftService, INotyfService notyfservice)
        {
            _AircraftService = AircraftService;
            _notyfservice = notyfservice;
        }

        public async Task<IActionResult> Index()
        {
            Response<List<AircraftDTO>> response = await _AircraftService.GetListAsync();

            if (!response.Succeed)
            {
                _notyfservice.Error(response.Message);
                return RedirectToAction("Index", "Home");
            }

            return View(response.Result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AircraftDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notyfservice.Error("Debe ajustar los errores de validación");
                return View(dto);
            }

            Response<AircraftDTO> response = await _AircraftService.CreateAsync(dto);

            if (!response.Succeed)
            {
                _notyfservice.Error(response.Message);
                return View(dto);
            }

            _notyfservice.Success(response.Message);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            Response<AircraftDTO> response = await _AircraftService.GetOneAsync(id);

            if (!response.Succeed)
            {
                _notyfservice.Error(response.Message);
                return RedirectToAction(nameof(Index));
            }

            return View(response.Result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] AircraftDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notyfservice.Error("Debe ajustar los errores de validación");
                return View(dto);
            }

            Response<AircraftDTO> response = await _AircraftService.UpdateAsync(dto);

            if (!response.Succeed)
            {
                _notyfservice.Error(response.Message);
                return View(dto);
            }

            _notyfservice.Success(response.Message);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Response<object> response = await _AircraftService.DeleteAsync(id);

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
