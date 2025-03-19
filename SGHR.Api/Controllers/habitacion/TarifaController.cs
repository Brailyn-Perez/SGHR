using Microsoft.AspNetCore.Mvc;
using SGHR.Application.DTos.habitacion.Tarifa;
using SGHR.Application.Interfaces.habitacion;

namespace SGHR.Api.Controllers.habitacion
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarifaController : ControllerBase
    {
        private readonly ITarifaService _service;

        public TarifaController(ITarifaService service)
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
        public async Task<IActionResult> Post(SaveTarifaDTO tarifa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _service.Save(tarifa);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Put(UpdateTarifaDTO tarifa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _service.Update(tarifa);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(RemoveTarifaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _service.Remove(dto);
            return Ok(result);
        }
    }
}
