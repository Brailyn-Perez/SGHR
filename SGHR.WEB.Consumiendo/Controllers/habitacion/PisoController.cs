using Microsoft.AspNetCore.Mvc;
using SGHR.WEB.Consumiendo.Models.habitacion.Piso;

namespace SGHR.WEB.Consumiendo.Controllers
{
    public class PisoController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "http://localhost:5017/api/Piso";

        public PisoController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Index
        public async Task<IActionResult> Index()
        {
            List<PisoViewModel> pisos = new();

            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<PisoViewModel>>(_apiUrl);
                if (response != null)
                {
                    pisos = response;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            return View(pisos);
        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PisoViewModel piso)
        {
            if (!ModelState.IsValid)
            {
                return View(piso);
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_apiUrl, piso);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Error al guardar el piso.";
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            return View(piso);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int id)
        {
            PisoViewModel piso = null;
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<PisoViewModel>(apiUrl);
                if (response != null)
                {
                    piso = response;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            if (piso == null)
            {
                return NotFound();
            }

            return View(piso);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PisoViewModel piso)
        {
            if (id != piso.IdPiso)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(piso);
            }

            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.PutAsJsonAsync(apiUrl, piso);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Error al actualizar el piso.";
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            return View(piso);
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int id)
        {
            PisoViewModel piso = null;
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<PisoViewModel>(apiUrl);
                if (response != null)
                {
                    piso = response;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            if (piso == null)
            {
                return NotFound();
            }

            return View(piso);
        }

        // POST: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Error al eliminar el piso.";
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
