using Microsoft.AspNetCore.Mvc;
using UsuariosMongoDB.Entities;
using UsuariosMongoDB.Repositories;

namespace UsuariosMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;

        public UsuarioController(IUsuarioService productService) =>
            this.usuarioService = productService;

        //[HttpGet]
        //public async Task<List<UsuarioDetails>> Get()
        //{
        //    return await usuarioService.UsuarioListAsync();

        //}

        [HttpGet("{usuarioId}")]
        public async Task<ActionResult<UsuarioDetails>> Get(int usuarioId)
        {
            var usuarioDetails = await usuarioService.GetUsuarioDetailsByIdAsync(usuarioId);

            if (usuarioDetails is null)
            {
                return NotFound();
            }

            return usuarioDetails;
        }

        [HttpGet()]
        public async Task<List<int>> GetUsuariosId(string nombre, string apellido, string nombreUsuario)
        {
            List<int> usuario = await usuarioService.GetUsuariosId(nombre, apellido, nombreUsuario);

            if (usuario is null)
            {
                return null;
            }

            return usuario.ToList();
        }

        [HttpPost]
        public async Task<IActionResult> Post(UsuarioDetails usuarioDetails)
        {
            await usuarioService.AddUsuarioAsync(usuarioDetails);

            return CreatedAtAction(nameof(Get), new { id = usuarioDetails.UserId }, usuarioDetails);
        }

        [HttpPut("{usuarioId}")]
        public async Task<IActionResult> Update(int usuarioId, UsuarioDetails usuarioDetails)
        {
            var usuarioDetail = await usuarioService.GetUsuarioDetailsByIdAsync(usuarioId);

            if (usuarioDetail is null)
            {
                return NotFound();
            }

            usuarioDetails.UserId = usuarioDetail.UserId;

            await usuarioService.UpdateUsuarioAsync(usuarioId, usuarioDetails);

            return Ok();
        }

        [HttpDelete("{usuarioId}")]
        public async Task<IActionResult> Delete(int usuarioId)
        {
            var usuarioDetails = await usuarioService.GetUsuarioDetailsByIdAsync(usuarioId);

            if (usuarioDetails is null)
            {
                return NotFound();
            }

            await usuarioService.DeleteUsuarioAsync(usuarioId);

            return Ok();
        }
    }
}
