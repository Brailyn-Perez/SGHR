using Microsoft.AspNetCore.Mvc;
using SGHR.WEB.Consumiendo.Models.Base;
using SGHR.WEB.Consumiendo.Models.habitacion.Habitacion;
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

        public async Task<IActionResult> Index()
        {
            List<PisoViewModel> pisos = new();

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<PisoViewModel>>>(_apiUrl);
                if (response != null)
                {
                    pisos = response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            return View(pisos);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            PisoViewModel piso = null;
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<PisoViewModel>>(apiUrl);
                if (response != null && response.Success)
                {
                    piso = response.Data;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePisoViewModel piso)
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

        public async Task<IActionResult> Edit(int id)
        {
            PisoViewModel piso = null;
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<PisoViewModel>>(apiUrl);
                if (response != null)
                {
                    piso = response.Data;
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

            UpdatePisoViewModel updatePiso = new()
            {
                IdPiso = piso.IdPiso,
                Descripcion = piso.Descripcion,
                Estado = piso.Estado
            };
            return View(updatePiso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdatePisoViewModel piso)
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

        public async Task<IActionResult> Delete(int id)
        {
            PisoViewModel piso = null;
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<PisoViewModel>>(apiUrl);
                if (response != null)
                {
                    piso = response.Data;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeletePisoViewModel deletePiso)
        {
            string apiUrl = $"{_apiUrl}/{deletePiso.IdPiso}";

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
