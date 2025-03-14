using Microsoft.AspNetCore.Mvc;
using SGHR.Application.DTos.reserva.Reserva;
using SGHR.Application.Interfaces.reserva;
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
        private readonly IReservaService _repository;

        public ReservaController(IReservaService repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> Get()
        {
            var reservas = await _repository.GeAll();
            return Ok(reservas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> Get(int id)
        {
            var reserva = await _repository.GeById(id);
            if (reserva == null)
            {
                return NotFound();
            }
            return Ok(reserva);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SaveReservaDTO reserva)
        {
            var result = await _repository.Save(reserva);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return CreatedAtAction(nameof(Get), reserva);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateReservaDTO reserva)
        {
            if (id != reserva.IdReserva)
            {
                return BadRequest();
            }

            var result = await _repository.Update(reserva);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _repository.GeById(id);
            var result = await _repository.Remove(entity.Data);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return NoContent();
        }
    }
}