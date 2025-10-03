using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Data;
using Proyecto_Aerolinea.Web.Services.Abstract;
using Proyecto_Aerolinea.Web.Services.Implementation;
using System.Collections.Generic;
using Proyecto_Aerolinea.Web.Models;
using AspNetCoreHero.ToastNotification.Abstractions;

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
        public IActionResult Index()
        {
            return View();
        }

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
            Response<FlightDTO> NewFlight = await _flightService.CreateAsync(dto);
            if (!NewFlight.Succeed)
            {
                _otyfService.Error("No se completo la creacion del vuelo");
            }
            return RedirectToAction("Index");
        }

        // GET: FlightsController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: FlightsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FlightsController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: FlightsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
