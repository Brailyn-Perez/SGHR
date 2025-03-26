using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGHR.Application.DTos.habitacion.EstadoHabitacion;
using SGHR.Application.Interfaces.habitacion;

namespace SGHR.Web.Controllers.habitacion
{
    public class EstadoHabitacionController : Controller
    {
        private readonly IEstadoHabitacionService _service;

        public EstadoHabitacionController(IEstadoHabitacionService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var estados = await _service.GeAll();
            return View(estados.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var estado = await _service.GeById(id);
            return View(estado.Data);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveEstadoHabitacionDTO saveEstado)
        {
            try
            {
                await _service.Save(saveEstado);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var estado = await _service.GeById(id);
            return View(estado.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateEstadoHabitacionDTO update)
        {
            try
            {
                update.IdEstadoHabitacion = id;
                await _service.Update(update);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var estado = await _service.GeById(id);
            return View(estado.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id,EstadoHabitacionDTO estadoDTO)
        {
            try
            {
                var estado = new RemoveEstadoHabitacionDTO
                {
                    IdEstadoHabitacion = id
                };
                await _service.Remove(estado);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
