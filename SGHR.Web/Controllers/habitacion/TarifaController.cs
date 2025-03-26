using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGHR.Application.Interfaces.habitacion;

namespace SGHR.Web.Controllers.habitacion
{
    public class TarifaController : Controller
    {
        private readonly ITarifaService _service;

        public TarifaController(ITarifaService service)
        {
            _service = service;
        }


        // GET: TarifaController
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: TarifaController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        // GET: TarifaController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: TarifaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TarifaController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }

        // POST: TarifaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TarifaController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }

        // POST: TarifaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
