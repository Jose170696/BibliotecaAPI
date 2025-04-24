using BibliotecaAPI.Models;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PrestamoController : Controller
    {
        private readonly PrestamoService _prestamoService;

        public PrestamoController(PrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        // GET: api/prestamo
        [HttpGet]
        public async Task<ActionResult<List<PrestamoModel>>> ObtenerPrestamos()
        {
            var prestamos = await _prestamoService.ObtenerPrestamosAsync();
            return Ok(prestamos);
        }

        // POST: api/prestamo
        [HttpPost]
        public async Task<ActionResult> RegistrarPrestamo([FromBody] PrestamoModel prestamo)
        {
            await _prestamoService.RegistrarPrestamoAsync(prestamo);
            return CreatedAtAction(nameof(ObtenerPrestamos), new { id = prestamo.Id }, prestamo);
        }

        // PUT: api/prestamo/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarPrestamo(int id, [FromBody] PrestamoModel prestamo)
        {
            var actualizado = await _prestamoService.ActualizarPrestamoAsync(id, prestamo);
            if (!actualizado) return NotFound();
            return NoContent();
        }

        // DELETE: api/prestamo/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarPrestamo(int id)
        {
            var eliminado = await _prestamoService.EliminarPrestamoAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
