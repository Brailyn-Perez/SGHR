using Microsoft.AspNetCore.Mvc;
using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Interfaces.habitacion;

namespace SGHR.Api.Controllers.habitacion
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoHabitacionController : ControllerBase
    {
        private readonly IEstadoHabitacionRepository _repository;

        public EstadoHabitacionController(IEstadoHabitacionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _repository.GetAllAsync(e => !e.Borrado);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID debe ser un número positivo.");
            }

            var estadoHabitacion = await _repository.GetEntityByIdAsync(id);
            if (estadoHabitacion == null)
            {
                return NotFound("El estado de habitación con el ID especificado no existe.");
            }

            return Ok(estadoHabitacion);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EstadoHabitacion estadoHabitacion)
        {
            if (estadoHabitacion == null)
            {
                return BadRequest("El estado de habitación no puede ser nulo.");
            }

            if (string.IsNullOrWhiteSpace(estadoHabitacion.Descripcion))
            {
                return BadRequest("La descripción no puede estar vacía.");
            }

            var result = await _repository.SaveEntityAsync(estadoHabitacion);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(Get), new { id = estadoHabitacion.IdEstadoHabitacion }, estadoHabitacion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EstadoHabitacion estadoHabitacion)
        {
            if (id <= 0 || estadoHabitacion == null || id != estadoHabitacion.IdEstadoHabitacion)
            {
                return BadRequest("Datos inválidos.");
            }

            if (string.IsNullOrWhiteSpace(estadoHabitacion.Descripcion))
            {
                return BadRequest("La descripción no puede estar vacía.");
            }

            var result = await _repository.UpdateEntityAsync(estadoHabitacion);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

    }
}