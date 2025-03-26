using Microsoft.AspNetCore.Mvc;
using SGHR.Application.DTos.habitacion.Categoria;
using SGHR.Application.Interfaces.habitacion;

namespace SGHR.Web.Controllers.habitacion
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _service;

        public CategoriaController(ICategoriaService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _service.GeAll();
            return View(categories.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await _service.GeById(id);
            return View(category.Data);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveCategoriaDTO categoriaDTO)
        {
            try
            {
                await _service.Save(categoriaDTO);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _service.GeById(id);
            UpdateCategoriaDTO updateCategoriaDTO = new UpdateCategoriaDTO
            {
                IdCategoria = category.Data.IdCategoria,
                Descripcion = category.Data.Descripcion,
                Estado = category.Data.Estado
            };
            return View(updateCategoriaDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateCategoriaDTO categoriaDTO)
        {
            try
            {
                categoriaDTO.IdCategoria = id;
                await _service.Update(categoriaDTO);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _service.GeById(id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, RemoveCategoriaDTO categoriaDTO)
        {
            try
            {
                categoriaDTO.IdCategoria = id;
                await _service.Remove(categoriaDTO);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
