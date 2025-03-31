using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGHR.Web.Comsuming.Api.MAL.Models.sevicios;
/*
 http://localhost:5017/api
 */
namespace SGHR.Web.Comsuming.Api.MAL.Controllers.sevicios
{
    public class ServicioController : Controller
    {
        HttpClient client;
        string baseurl = "http://localhost:5017/api";
        string geturl = "/Reserva";
        string getidurl = "/Servicio/"; // + id
        string posturl = "/Servicio";
        string puturl = "/Servicio/"; // + id
        string deleteurl = "/Servicio/"; // + id

        public ServicioController(HttpClient client)
        {
            this.client = client;
        }
        // GET: ServicioController
        public async Task<IActionResult> Index()
        {
            List<ServicioModel> servicios = new List<ServicioModel>();
            var response = await client.GetAsync(baseurl + geturl);
            if (response.IsSuccessStatusCode)
            {
                servicios = await response.Content.ReadFromJsonAsync<List<ServicioModel>>();
            }
            return View(servicios);
        }

        // GET: ServicioController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ServicioModel servicio = new ServicioModel();
            var response = await client.GetAsync(baseurl + getidurl + id);
            if (response.IsSuccessStatusCode)
            {
                servicio = await response.Content.ReadFromJsonAsync<ServicioModel>();
            }
            return View(servicio);
        }

        // GET: ServicioController/Create
        public async Task<IActionResult> Create()
        {
            var model = new ServicioModel();
            return View(model);
        }

        // POST: ServicioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServicioModel model)
        {
            try
            {
                var response = await client.PostAsJsonAsync(baseurl + posturl, model);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: ServicioController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ServicioModel servicio = new ServicioModel();
            var response = await client.GetAsync(baseurl + getidurl + id);
            if (response.IsSuccessStatusCode)
            {
                servicio = await response.Content.ReadFromJsonAsync<ServicioModel>();
            }
            ServicioModel editService = new ServicioModel
            {
                IdServicio = servicio.IdServicio,
                Nombre = servicio.Nombre,
                Descripcion = servicio.Descripcion
            };
            return View(editService);
        }

        // POST: ServicioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ServicioModel model)
        {
            if (id != model.IdServicio)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var response = client.PutAsJsonAsync(baseurl + puturl + id, model).Result;
                if (response.IsSuccessStatusCode) 
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(HttpRequestException ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
        // POST: ServicioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                var response = await client.DeleteAsync(baseurl + deleteurl + id);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            catch(HttpRequestException ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
