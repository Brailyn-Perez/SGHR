using Microsoft.AspNetCore.Mvc;
using SGHR.Domain.Entities.usuario;
using SGHR.Persistence.Interfaces.usuario;


namespace SGHR.Api.Controllers.usuario
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolUsuarioController : ControllerBase
    {
        private readonly IRolUsuarioRepository _repository;

        public RolUsuarioController(IRolUsuarioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetRolUsuario")]
        public async Task<IActionResult> Get()
        {
            var RolUsuario = await _repository.GetAllAsync();
            return Ok(RolUsuario);
        }

        [HttpGet("GetRolUsuarioById")]
        public async Task<IActionResult> Get(int id)
        {
            var RolUsuario = await _repository.GetEntityByIdAsync(id);
            if (RolUsuario == null) 
            {
                return NotFound();
            }
            return Ok(RolUsuario);
        }

        [HttpPost("SaveRolUsuario")]
        public async Task<IActionResult> Post([FromBody] RolUsuario rolUsuario)
        {
            var RolUsuario = await _repository.SaveEntityAsync(rolUsuario);
            return Ok(RolUsuario);
        }

        [HttpPut("UpdateRolUsuario")]
        public async Task<IActionResult> Put([FromBody] RolUsuario rolUsuario)
        {
            var RolUsuario = await _repository.UpdateEntityAsync(rolUsuario);
            return Ok(RolUsuario);
        }
    }
}
