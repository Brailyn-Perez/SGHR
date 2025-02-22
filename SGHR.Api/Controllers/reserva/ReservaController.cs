using Microsoft.AspNetCore.Mvc;
using SGHR.Domain.Entities.reserva;
using SGHR.Persistence.Interfaces.reserva;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGHR.Api.Controllers.reserva
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaRepository _repository;

        public ReservaController(IReservaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> Get()
        {
            var reservas = await _repository.GetAllAsync();
            return Ok(reservas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> Get(int id)
        {
            var reserva = await _repository.GetEntityByIdAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            return Ok(reserva);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Reserva reserva)
        {
            var result = await _repository.RealizarReservaAsync(reserva);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return CreatedAtAction(nameof(Get), new { id = reserva.IdReserva }, reserva);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Reserva reserva)
        {
            if (id != reserva.IdReserva)
            {
                return BadRequest();
            }

            var result = await _repository.UpdateEntityAsync(reserva);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _repository.CancelarReservaAsync(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return NoContent();
        }
    }
}