using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult Index()
        {

            return View();
        }

        // GET: TicketController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TicketController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: TicketController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TicketController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // POST: TicketController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
