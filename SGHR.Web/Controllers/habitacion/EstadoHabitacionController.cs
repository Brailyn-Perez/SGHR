using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


        // GET: EstadoHabitacionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EstadoHabitacionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EstadoHabitacionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoHabitacionController/Create
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

        // GET: EstadoHabitacionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EstadoHabitacionController/Edit/5
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

        // GET: EstadoHabitacionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EstadoHabitacionController/Delete/5
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
