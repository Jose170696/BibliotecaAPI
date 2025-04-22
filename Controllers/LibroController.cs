using Microsoft.AspNetCore.Mvc;
using BibliotecaAPI.Models;
using BibliotecaAPI.Services;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibroController : Controller
    {
        private readonly LibroService _libroService;

        public LibroController(LibroService libroService)
        {
            _libroService = libroService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroModel>>> GetLibros()
        {
            var libros = await _libroService.ObtenerLibrosAsync();
            return Ok(libros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroModel>> ObtenerLibroPorId(int id)
        {
            var libro = await _libroService.ObtenerLibroPorIdAsync(id);
            if (libro == null)
            {
                return NotFound("No se encontro este libro");
            }
            return Ok(libro);
        }

        [HttpPost]
        public async Task<ActionResult> CrearLibro([FromBody] LibroModel libro)
        {
            await _libroService.CrearLibroAsync(libro);
            return Ok("Libro agregado de manera correcta");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarLibro(int id, [FromBody] LibroModel libro)
        {
            var actualizado = await _libroService.ActualizarLibroAsync(id, libro);
            if (!actualizado)
            {
                return NotFound("Libro no se encuentra");
            }
            return Ok("Se actualizo correctamente el libro");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarLibro(int id)
        {
            var eliminado = await _libroService.EliminarLibroAsync(id);
            if (!eliminado)
            {
                return NotFound("No se puede eliminar libro no existente");
            }
            return Ok("Libro eliminado de manera correcta");
        }



        public IActionResult Index()
        {
            return View();
        }
    }
}
