using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Services.Abstract;
using System.Threading.Tasks;

namespace Proyecto_Aerolinea.Web.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketServices _ticketservices;
        private readonly INotyfService _otyService;
        public TicketController(INotyfService otyService, ITicketServices ticketservices)
        {
            _ticketservices = ticketservices;
            _otyService = otyService;
        }
        // GET: TicketController
        public async Task<IActionResult> Index()
        {
            Response<List<TicketDTO>> response = await _ticketservices.MyGetlistAsync();

            if (!response.Succeed)
            {
                _otyService.Error("No se pudo obtener el listado de Ticket");
                return RedirectToAction("Index", "Home");
            }

            return View(response.Result);
        }

        // GET: TicketController/Create
        public IActionResult Create()
        {
            return View();
        }

        //GET
        public IActionResult Details([FromRoute]TicketDTO dto)
        {
            return View(dto);
        }

        // POST: TicketController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]TicketDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _otyService.Error("No se cumplieron los requisitos de creacion");
                return RedirectToAction("Index");
            }

            Response<TicketDTO> response = await _ticketservices.MyCreateAsync(dto);
            if (!response.Succeed)
            {
                _otyService.Error("El ticket no se logro crear");
                return RedirectToAction("Index");
            }
            return View(response.Result);

        }

        // GET: TicketController/Edit/5
        public async Task<IActionResult> Edit([FromRoute]Guid id)
        {
            Response<TicketDTO> response = await _ticketservices.MyGetOneAsync(id);

            if (!response.Succeed)
            {
                _otyService.Error($"No se encontro el ticket {id}");
                return RedirectToAction("Index");
            }

            return View(response.Result);

        }

        // POST: TicketController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm]TicketDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _otyService.Error("No se complio con los paramtros establecidos");
                return RedirectToAction("Index");
            }

            Response<TicketDTO> response = await _ticketservices.MyUpdateAsync(dto);

            if (!response.Succeed)
            {
                _otyService.Error($"El Ticket {dto.Id} no se pudo actualizar");
                return RedirectToAction("Index");
            }

            _otyService.Success($"El ticket {dto.Id} fue actualizado correctamente");
            return RedirectToAction("Index");

        }

        // POST: TicketController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            Response<object> response = await _ticketservices.MyDeleteAsync(id);

            if (!response.Succeed)
            {
                _otyService.Error("El ticket no se pudo eliminar de la base de datos");
                return RedirectToAction("Index");
            }

            _otyService.Success("El ticket fue eliminado correctamente de la base de datos");
            return RedirectToAction("Index");

        }
    }
}
