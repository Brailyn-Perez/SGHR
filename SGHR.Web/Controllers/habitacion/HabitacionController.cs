﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGHR.Application.Interfaces.habitacion;

namespace SGHR.Web.Controllers.habitacion
{
    public class HabitacionController : Controller
    {
        private readonly IHabitacionService _service;

        public HabitacionController(IHabitacionService service)
        {
            _service = service;
        }

        // GET: HabitacionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HabitacionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HabitacionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HabitacionController/Create
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

        // GET: HabitacionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HabitacionController/Edit/5
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

        // GET: HabitacionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HabitacionController/Delete/5
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
