using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Services.Abstract;
using Proyecto_Aerolinea.Web.Services.Implementation;
using System.Threading.Tasks;

namespace Proyecto_Aerolinea.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FlightsController : Controller
    {
        private readonly IFlightService _Flightservice;
        private readonly IAirportService _Airportservice;
        private readonly IAircraftService _Aircraftservice;
        private readonly INotyfService _notyfservice;

        public FlightsController(IFlightService Flightservice, IAirportService AirportService, IAircraftService AircraftService, INotyfService notyfservice)
        {
            _Flightservice = Flightservice;
            _Airportservice = AirportService;
            _Aircraftservice = AircraftService;
            _notyfservice = notyfservice;
        }

        public async Task<IActionResult> Index()
        {
            Response<List<FlightDTO>> response = await _Flightservice.MyGetListAsync();

            if (!response.Succeed)
            {
                _notyfservice.Error(response.Message);
                return RedirectToAction("Index", "Home");
            }

            return View(response.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var airportsResponse = await _Airportservice.GetListAsync();
            var aircraftsResponse = await _Aircraftservice.GetListAsync();

            if (!airportsResponse.Succeed || !aircraftsResponse.Succeed)
            {
                _notyfservice.Error("Error al cargar los datos");
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Airports = new SelectList(airportsResponse.Result, "Id", "AirportName");
            ViewBag.Aircrafts = new SelectList(aircraftsResponse.Result, "Id", "Model");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] FlightDTO dto)
        {
            Response<AirportDTO> originAirport = await _Airportservice.GetOneAsync(dto.OriginAirportId);
            Response<AirportDTO> destinationAirport = await _Airportservice.GetOneAsync(dto.DestinationAirportId);
            Response<AircraftDTO> aircraft = await _Aircraftservice.GetOneAsync(dto.AircraftId);

            if (!ModelState.IsValid)
            {
                Response<List<AirportDTO>> airportsResponse = await _Airportservice.GetListAsync();
                Response<List<AircraftDTO>> aircraftsResponse = await _Aircraftservice.GetListAsync();
                ViewBag.Airports = new SelectList(airportsResponse.Result, "Id", "AirportName");
                ViewBag.Aircrafts = new SelectList(aircraftsResponse.Result, "Id", "Model");

                _notyfservice.Error("Debe ajustar los errores de validación");
                return View(dto);
            }

            Response<FlightDTO> response = await _Flightservice.MyCreateAsync(dto);

            if (!response.Succeed)
            {
                Response<List<AirportDTO>> airportsResponse = await _Airportservice.GetListAsync();
                Response<List<AircraftDTO>> aircraftsResponse = await _Aircraftservice.GetListAsync();
                ViewBag.Airports = new SelectList(airportsResponse.Result, "Id", "AirportName");
                ViewBag.Aircrafts = new SelectList(aircraftsResponse.Result, "Id", "Model");

                _notyfservice.Error(response.Message);
                return View(dto);
            }

            _notyfservice.Success(response.Message);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            var flightResponse = await _Flightservice.MyGetOneAsync(id);
            var airportsResponse = await _Airportservice.GetListAsync();
            var aircraftsResponse = await _Aircraftservice.GetListAsync();

            if (!flightResponse.Succeed)
            {
                _notyfservice.Error(flightResponse.Message);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Airports = new SelectList(airportsResponse.Result, "Id", "AirportName");
            ViewBag.Aircrafts = new SelectList(aircraftsResponse.Result, "Id", "Model");

            return View(flightResponse.Result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] FlightDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var airportsResponse = await _Airportservice.GetListAsync();
                var aircraftsResponse = await _Aircraftservice.GetListAsync();
                ViewBag.Airports = new SelectList(airportsResponse.Result, "Id", "AirportName");
                ViewBag.Aircrafts = new SelectList(aircraftsResponse.Result, "Id", "Model");

                _notyfservice.Error("Debe ajustar los errores de validación");
                return View(dto);
            }

            var response = await _Flightservice.UpdateAsync(dto);

            if (!response.Succeed)
            {
                var airportsResponse = await _Airportservice.GetListAsync();
                var aircraftsResponse = await _Aircraftservice.GetListAsync();
                ViewBag.Airports = new SelectList(airportsResponse.Result, "Id", "AirportName");
                ViewBag.Aircrafts = new SelectList(aircraftsResponse.Result, "Id", "Model");

                _notyfservice.Error(response.Message);
                return View(dto);
            }

            _notyfservice.Success(response.Message);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Response<object> response = await _Flightservice.MyDeleteAsync(id);

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
