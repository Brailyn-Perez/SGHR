using Microsoft.AspNetCore.Mvc;
using SGHR.Application.DTos.habitacion.Categoria;
using SGHR.Application.Interfaces.habitacion;
namespace SGHR.Api.Controllers.habitacion
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriaController(ICategoriaService Service)
        {
            _service = Service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GeAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GeById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(SaveCategoriaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.Save(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateCategoriaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(RemoveCategoriaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.Remove(dto);
            return Ok(result);
        }
    }
}
