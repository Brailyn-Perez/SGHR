using Microsoft.AspNetCore.Mvc;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.usuario;
using SGHR.Persistence.Interfaces.usuario;


namespace SGHR.Api.Controllers.usuario
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _repository;

        public ClienteController(IClienteRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetCliente")]
        public async Task<IActionResult> Get()
        {
            var Cliente = await _repository.GetAllAsync();
            return Ok(Cliente);
        }

        [HttpGet("GetClienteById")]
        public async Task<IActionResult> Get(int id)
        {
            var Cliente = await _repository.GetEntityByIdAsync(id);
            if (Cliente == null) 
            {
                return NotFound();
            }
            return Ok(Cliente);
        }

        [HttpPost("SaveCliente")]
        public async Task<IActionResult> Post([FromBody] Cliente Cliente)
        {
            var cliente = await _repository.SaveEntityAsync(Cliente);
            return Ok(cliente);
        }

        [HttpPut("UpdateCliente")]
        public async Task<IActionResult> Put([FromBody] Cliente Cliente)
        {
            var cliente = await _repository.UpdateEntityAsync(Cliente);
            return Ok(Cliente);
        }
    }
}
