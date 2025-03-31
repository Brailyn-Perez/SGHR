using Microsoft.AspNetCore.Mvc;
using SGHR.Application.DTos.reserva.Reserva;
using SGHR.Application.Interfaces.reserva;
using SGHR.Domain.Base;
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
        private readonly IReservaService _service;

        public ReservaController(IReservaService repository)
        {
            _service = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> Get()
        {
            var reservas = await _service.GeAll();
            return Ok(reservas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> Get(int id)
        {
            List<OperationResult> resultado = new();
            OperationResult result = new();
            result = await _service.GeById(id);
            if (result.Data == null)
            {
                return NotFound();
            }
            resultado.Add(result);

            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SaveReservaDTO reserva)
        {
            var result = await _service.Save(reserva);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            List<OperationResult> resultados = new();
            resultados.Add(result);
            return CreatedAtAction(nameof(Get), resultados.ToList());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateReservaDTO reserva)
        {
            if (id != reserva.IdReserva)
            {
                return BadRequest();
            }

            var result = await _service.Update(reserva);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _service.GeById(id);
            var result = await _service.Remove(entity.Data = new RemoveReservaDTO()
            {
                IdReserva = entity.Data.IdReserva
            });
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return NoContent();
        }
    }
}