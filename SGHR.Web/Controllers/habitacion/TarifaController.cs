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
        public ActionResult Index()
        {
            return View();
        }

        // GET: TarifaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TarifaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TarifaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TarifaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TarifaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
