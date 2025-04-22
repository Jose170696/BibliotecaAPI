using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    public class PrestamoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
