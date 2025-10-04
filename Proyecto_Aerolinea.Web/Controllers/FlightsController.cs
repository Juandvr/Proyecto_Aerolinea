using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Data;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Models;
using Proyecto_Aerolinea.Web.Services.Abstract;
using Proyecto_Aerolinea.Web.Services.Implementation;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Proyecto_Aerolinea.Web.Controllers
{
    public class FlightsController : Controller
    {
        private readonly IFlightService _flightService;
        private readonly INotyfService _otyfService;
        public FlightsController(IFlightService flightService, INotyfService otyfService)
        {
            _flightService=flightService;
            _otyfService = otyfService;
        }
        // GET: FlightsController
        /*public IActionResult Index()
        {
            return View();
        }*/

        [HttpGet]
        public async Task<IActionResult> Available(/*[FromQuery] Pages pages*/)
        {
            Response<List<FlightDTO>> response = await _flightService.MyGetListAsync();

            if (!response.Succeed)
            {
                _otyfService.Error(response.Message);
                return RedirectToAction("Index", "Home");
            }

            return View(response.Result);
        }

        // GET: FlightsController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: FlightsController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FlightsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]FlightDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _otyfService.Error("No se cumplen con las validaciones");
                return View();
            }
            Response<FlightDTO> NewFlight = await _flightService.MyCreateAsync(dto);
            if (!NewFlight.Succeed)
            {
                _otyfService.Error("No se completo la creacion del vuelo");
            }
            return RedirectToAction("Available");
        }

        // GET: FlightsController/Edit/5
        public async Task<IActionResult> Edit([FromRoute]Guid id)
        {
            Response<FlightDTO> edit = await _flightService.MyGetOneAsync(id);

            if (!edit.Succeed)
            {
                _otyfService.Error(edit.Message);
                return RedirectToAction("Availabel");
            }

            return View(edit.Result);
        }

        // POST: FlightsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm]FlightDTO dto)
        {
            Response<FlightDTO> Update = await _flightService.UpdateAsync(dto);
            if (!Update.Succeed)
            {
                _otyfService.Error(Update.Message);
                return RedirectToAction("Available");
            }
            _otyfService.Success(Update.Message);
            return RedirectToAction("Available");
        }

        // POST: FlightsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            Response<object> del = await _flightService.MyDeleteAsync(id);
            
            if (!del.Succeed)
            {
                _otyfService.Error($"El Vuelo con ID: {id} no se pudo borrar");
                return RedirectToAction("Available");
            }
            _otyfService.Success($"El Vuelo con ID: {id} se elimino correctamente");
            return RedirectToAction("Available");
        }
    }
}
