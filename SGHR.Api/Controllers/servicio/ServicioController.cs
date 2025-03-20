using Microsoft.AspNetCore.Mvc;
using SGHR.Application.DTos.servicio.Servicio;
using SGHR.Application.Interfaces.sevicio;
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
        private readonly IServiciosService _repository;

        public ServicioController(IServiciosService repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servicios>>> Get()
        {
            var servicios = await _repository.GeAll();
            return Ok(servicios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Servicios>> Get(int id)
        {
            var servicio = await _repository.GeById(id);
            if (servicio == null)
            {
                return NotFound();
            }
            return Ok(servicio);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SaveServicioDTO servicio)
        {
            var result = await _repository.Save(servicio);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return CreatedAtAction(nameof(Get), servicio);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateServicioDTO servicio)
        {
            if (id != servicio.IdServicio)
            {
                return BadRequest();
            }

            var result = await _repository.Update(servicio);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var servicio = await _repository.GeById(id);
            if (servicio == null)
            {
                return NotFound();
            }

            var result = _repository.Remove(servicio.Data = new RemoveServicioDTO(){
                IdServicio = servicio.Data.IdServicio
            });
            return NoContent();
        }

        //[HttpPost("AsociarServicioACategoria")]
        //public async Task<ActionResult> AsociarServicioACategoria(int idServicio, int idCategoria)
        //{
        //    var result = await _repository.AsociarServicioACategoriaAsync(idServicio, idCategoria);
        //    if (!result.Success)
        //    {
        //        return BadRequest(result.Message);
        //    }
        //    return Ok(result.Message);
        //}

        //[HttpGet("ObtenerServiciosPorCategoria/{idCategoria}")]
        //public async Task<ActionResult<List<Servicios>>> ObtenerServiciosPorCategoria(int idCategoria)
        //{
        //    var servicios = await _repository.ObtenerServiciosPorCategoriaAsync(idCategoria);
        //    return Ok(servicios);
        //}
    }
}