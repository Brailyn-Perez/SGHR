using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGHR.Application.Interfaces.habitacion;

namespace SGHR.Web.Controllers.habitacion
{
    public class PisoController : Controller
    {
        private readonly IPisoService _service;

        public PisoController(IPisoService service)
        {
            _service = service;
        }

        // GET: PisoController
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: PisoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        // GET: PisoController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: PisoController/Create
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

        // GET: PisoController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }

        // POST: PisoController/Edit/5
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

        // GET: PisoController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }

        // POST: PisoController/Delete/5
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
