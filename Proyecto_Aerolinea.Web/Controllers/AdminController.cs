using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Aerolinea.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
