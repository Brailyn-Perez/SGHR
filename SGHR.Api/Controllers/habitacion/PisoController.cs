using Microsoft.AspNetCore.Mvc;
using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Interfaces.habitacion;


namespace SGHR.Api.Controllers.habitacion
{
    [Route("api/[controller]")]
    [ApiController]
    public class PisoController : ControllerBase
    {
        private readonly IPisoRepository _repository;

        public PisoController(IPisoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var value = await _repository.GetAllAsync(x => x.Borrado == false);
            return StatusCode(StatusCodes.Status200OK, new { value });
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var value = await _repository.GetEntityByIdAsync(id);

            return StatusCode(StatusCodes.Status200OK , new {value});
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Piso piso)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var response = await _repository.SaveEntityAsync(piso);
            return StatusCode(StatusCodes.Status200OK, new {piso});
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Piso piso)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            piso.IdPiso = id;
            piso.FechaActualizacion = DateTime.Now;
            piso.UsuarioActualizacion = 1;

            var response = await _repository.UpdateEntityAsync(piso);
            return StatusCode(StatusCodes.Status200OK, new { piso });
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _repository.DeletePiso(id);
            return StatusCode(StatusCodes.Status200OK, new { response });
        }
    }
}
