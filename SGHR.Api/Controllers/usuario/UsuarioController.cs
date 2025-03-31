using Microsoft.AspNetCore.Mvc;
using SGHR.Application.DTos.usuario.Usuario;
using SGHR.Application.Interfaces.usuario;
using SGHR.Domain.Entities.usuario;
using SGHR.Persistence.Interfaces.usuario;


namespace SGHR.Api.Controllers.usuario
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService repository)
        {
            _service = repository;
        }
        
        [HttpGet("GetUsuario")]
        public async Task<IActionResult> Get()
        {
           var Usuario = await _service.GeAll();
            return Ok(Usuario);
        }

        [HttpGet("GetUsuarioById")]
        public async Task<IActionResult> Get(int id)
        {
            var Usuario = await _service.GeById(id);
            if (Usuario == null) 
            {
                return NotFound();
            }
            return Ok(Usuario);
        }

        [HttpPost("SaveUsuario")]
        public async Task<IActionResult> Post([FromBody] SaveUsuarioDTo usuario)
        {
            var Usuario = await _service.Save(usuario);
            return Ok(Usuario);
        }

        [HttpPut("UpdateUsuario")]
        public async Task<IActionResult> Put([FromBody] UpdateUsuarioDTo usuario)
        {
            var Usuario = await _service.Update(usuario);
            return Ok(Usuario);
        }
    }
}
