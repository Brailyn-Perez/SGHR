using Microsoft.AspNetCore.Mvc;
using SGHR.Domain.Entities.usuario;
using SGHR.Persistence.Interfaces.usuario;


namespace SGHR.Api.Controllers.usuario
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioController(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetUsuario")]
        public async Task<IActionResult> Get()
        {
           var Usuario = await _repository.GetAllAsync();
            return Ok(Usuario);
        }

        [HttpGet("GetUsuarioById")]
        public async Task<IActionResult> Get(int id)
        {
            var Usuario = await _repository.GetEntityByIdAsync(id);
            if (Usuario == null) 
            {
                return NotFound();
            }
            return Ok(Usuario);
        }

        [HttpPost("SaveUsuario")]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            var Usuario = await _repository.SaveEntityAsync(usuario);
            return Ok(Usuario);
        }

        [HttpPut("UpdateUsuario")]
        public async Task<IActionResult> Put([FromBody] Usuario usuario)
        {
            var Usuario = await _repository.UpdateEntityAsync(usuario);
            return Ok(Usuario);
        }
    }
}
