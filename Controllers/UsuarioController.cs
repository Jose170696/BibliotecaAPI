using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Mvc;
using BibliotecaAPI.Models;
using Microsoft.OpenApi.Writers;
namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsuarioController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]

        public async Task<ActionResult<List<UsuarioModel>>> ObtenerUsusarios()
        {
            var usuarios = await _usuarioService.ObtenerUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpGet("(id)")]

        public async Task<ActionResult<UsuarioModel>> ObtenerUsuariosPorId(int id)
        {
            var usuario = await _usuarioService.ObtenerUsuarioPorIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }


        [HttpPost("validar")]

        public async Task<ActionResult<UsuarioModel>> ValidarUsuario([FromQuery] string correo, [FromQuery] string clave)
        {
            var usuario = await _usuarioService.ValidarUsuarioAsync(correo, clave);
            if (usuario == null)
            {
                return Unauthorized();
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult> CrearUsuario([FromBody] UsuarioModel usuario)
        {
            await _usuarioService.CrearUsuarioAsync(usuario);
            return CreatedAtAction(nameof(ObtenerUsuariosPorId), new { id = usuario.Id }, usuario);
        }
        [HttpPut("{id}")]

        public async Task<ActionResult> ActualizarUsuario(int id, [FromBody] UsuarioModel usuario)
        {
            var actualizado = await _usuarioService.ActualizarUsuarioAsync(id, usuario);
            if (!actualizado)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> EliminarUsuario(int id)
        {
            var eliminado = await _usuarioService.EliminarUsuarioAsync(id);
            if (!eliminado)

            {
                return NotFound();
            }
            return NoContent();
        }
    }
}


   
  