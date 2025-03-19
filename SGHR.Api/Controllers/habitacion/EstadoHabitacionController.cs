using Microsoft.AspNetCore.Mvc;
using SGHR.Application.DTos.habitacion.EstadoHabitacion;
using SGHR.Application.Interfaces.habitacion;
using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Interfaces.habitacion;


namespace SGHR.Api.Controllers.habitacion
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoHabitacionController : ControllerBase
    {
        private readonly IEstadoHabitacionService _service;

        public EstadoHabitacionController(IEstadoHabitacionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GeAll();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GeById(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Post(SaveEstadoHabitacionDTO estadoHabitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.Save(estadoHabitacion);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Put(UpdateEstadoHabitacionDTO estadoHabitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.Update(estadoHabitacion);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(RemoveEstadoHabitacionDTO estadoHabitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.Remove(estadoHabitacion);
            return Ok(result);
        }

    }
}
