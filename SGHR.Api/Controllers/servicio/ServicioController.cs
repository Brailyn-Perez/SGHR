using Microsoft.AspNetCore.Mvc;
using SGHR.Domain.Entities.servicio;
using SGHR.Persistence.Interfaces.servicio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGHR.Api.Controllers.servicio
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly IServicioRepository _repository;

        public ServicioController(IServicioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servicios>>> Get()
        {
            var servicios = await _repository.GetAllAsync();
            return Ok(servicios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Servicios>> Get(int id)
        {
            var servicio = await _repository.GetEntityByIdAsync(id);
            if (servicio == null)
            {
                return NotFound();
            }
            return Ok(servicio);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Servicios servicio)
        {
            var result = await _repository.SaveEntityAsync(servicio);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return CreatedAtAction(nameof(Get), new { id = servicio.IdServicio }, servicio);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Servicios servicio)
        {
            if (id != servicio.IdServicio)
            {
                return BadRequest();
            }

            var result = await _repository.UpdateEntityAsync(servicio);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var servicio = await _repository.GetEntityByIdAsync(id);
            if (servicio == null)
            {
                return NotFound();
            }

            var result = await _repository.UpdateEntityAsync(servicio);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return NoContent();
        }

        [HttpPost("AsociarServicioACategoria")]
        public async Task<ActionResult> AsociarServicioACategoria(int idServicio, int idCategoria)
        {
            var result = await _repository.AsociarServicioACategoriaAsync(idServicio, idCategoria);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }

        [HttpGet("ObtenerServiciosPorCategoria/{idCategoria}")]
        public async Task<ActionResult<List<Servicios>>> ObtenerServiciosPorCategoria(int idCategoria)
        {
            var servicios = await _repository.ObtenerServiciosPorCategoriaAsync(idCategoria);
            return Ok(servicios);
        }
    }
}