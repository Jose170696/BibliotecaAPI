using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    public class LibroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
