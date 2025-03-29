using Microsoft.AspNetCore.Mvc;
using SGHR.WEB.Consumiendo.Models.habitacion.Tarifa;

namespace SGHR.WEB.Consumiendo.Controllers
{
    public class TarifaController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "http://localhost:5017/api/Tarifa";

        public TarifaController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Index
        public async Task<IActionResult> Index()
        {
            List<TarifaViewModel> tarifas = new();

            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<TarifaViewModel>>(_apiUrl);
                if (response != null)
                {
                    tarifas = response;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            return View(tarifas);
        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TarifaViewModel tarifa)
        {
            if (!ModelState.IsValid)
            {
                return View(tarifa);
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_apiUrl, tarifa);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Error al guardar la tarifa.";
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            return View(tarifa);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int id)
        {
            TarifaViewModel tarifa = null;
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<TarifaViewModel>(apiUrl);
                if (response != null)
                {
                    tarifa = response;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            if (tarifa == null)
            {
                return NotFound();
            }

            return View(tarifa);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TarifaViewModel tarifa)
        {
            if (id != tarifa.IdTarifa)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(tarifa);
            }

            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.PutAsJsonAsync(apiUrl, tarifa);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Error al actualizar la tarifa.";
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            return View(tarifa);
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int id)
        {
            TarifaViewModel tarifa = null;
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<TarifaViewModel>(apiUrl);
                if (response != null)
                {
                    tarifa = response;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            if (tarifa == null)
            {
                return NotFound();
            }

            return View(tarifa);
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
                    ViewBag.Error = "Error al eliminar la tarifa.";
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
