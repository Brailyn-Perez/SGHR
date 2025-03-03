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
            var value = await _repository.GetAllAsync(x => x.Borrado == false);
            return StatusCode(StatusCodes.Status200OK, new { value });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var value = await _repository.GetEntityByIdAsync(id);

            return StatusCode(StatusCodes.Status200OK, new { value });
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EstadoHabitacion estadoHabitacion)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var response = await _repository.SaveEntityAsync(estadoHabitacion);
            return StatusCode(StatusCodes.Status200OK, new { estadoHabitacion });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EstadoHabitacion estadoHabitacion)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            estadoHabitacion.IdEstadoHabitacion = id;
            estadoHabitacion.FechaActualizacion = DateTime.Now;
            estadoHabitacion.UsuarioActualizacion = 1;

            var response = await _repository.UpdateEntityAsync(estadoHabitacion);
            return StatusCode(StatusCodes.Status200OK, new { estadoHabitacion });
        }
    }
}
