using Microsoft.AspNetCore.Mvc;
using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Interfaces.habitacion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGHR.Api.Controllers.habitacion
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitacionController : ControllerBase
    {
        private readonly IHabitacionRepository _repository;

        public HabitacionController(IHabitacionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _repository.GetAllAsync(h => !h.Borrado);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID debe ser un número positivo.");
            }

            var habitacion = await _repository.GetEntityByIdAsync(id);
            if (habitacion == null)
            {
                return NotFound("La habitación con el ID especificado no existe.");
            }

            return Ok(habitacion);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Habitacion habitacion)
        {
            if (habitacion == null)
            {
                return BadRequest("La habitación no puede ser nula.");
            }

            if (string.IsNullOrWhiteSpace(habitacion.Detalle))
            {
                return BadRequest("La descripción no puede estar vacía.");
            }

            var result = await _repository.SaveEntityAsync(habitacion);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(Get), new { id = habitacion.IdHabitacion }, habitacion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Habitacion habitacion)
        {
            if (id <= 0 || habitacion == null || id != habitacion.IdHabitacion)
            {
                return BadRequest("Datos inválidos.");
            }

            if (string.IsNullOrWhiteSpace(habitacion.Detalle))
            {
                return BadRequest("La descripción no puede estar vacía.");
            }

            var result = await _repository.UpdateEntityAsync(habitacion);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID debe ser un número positivo.");
            }

            var result = await _repository.DeleteHabitacion(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}