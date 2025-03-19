using Microsoft.AspNetCore.Mvc;
using SGHR.Application.DTos.habitacion.Piso;
using SGHR.Application.Interfaces.habitacion;


namespace SGHR.Api.Controllers.habitacion
{
    [Route("api/[controller]")]
    [ApiController]
    public class PisoController : ControllerBase
    {
        private readonly IPisoService _service;

        public PisoController(IPisoService service)
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
        public async Task<IActionResult> Post(SavePisoDTO piso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _service.Save(piso);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdatePisoDTO piso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _service.Update(piso);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(RemovePisoDTO dto)
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
