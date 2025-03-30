using Microsoft.AspNetCore.Mvc;
using SGHR.WEB.Consumiendo.Models.Base;
using SGHR.WEB.Consumiendo.Models.habitacion.EstadoHabitacion;
using SGHR.WEB.Consumiendo.Models.habitacion.Habitacion;

namespace SGHR.WEB.Consumiendo.Controllers
{
    public class EstadoHabitacionController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "http://localhost:5017/api/EstadoHabitacion";

        public EstadoHabitacionController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            List<EstadoHabitacionViewModel> estados = new();

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<EstadoHabitacionViewModel>>>(_apiUrl);
                if (response != null)
                {
                    estados = response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            return View(estados);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            EstadoHabitacionViewModel estado = null;
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<EstadoHabitacionViewModel>>(apiUrl);
                if (response != null && response.Success)
                {
                    estado = response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEstado(CreateEstadoHabitacionViewModel estado)
        {
            if (!ModelState.IsValid)
            {
                return View(estado);
            }

            try
            {

                var response = await _httpClient.PostAsJsonAsync(_apiUrl, estado);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Error al guardar el estado de la habitación.";
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            return View(estado);
        }

        public async Task<IActionResult> Edit(int id)
        {
            EstadoHabitacionViewModel estado = null;
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<EstadoHabitacionViewModel>>(apiUrl);
                if (response != null)
                {
                    estado = response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            if (estado == null)
            {
                return NotFound();
            }
            UpdateEstadoHabitacionViewModel updateEstado = new()
            {
                IdEstadoHabitacion = id,
                Descripcion = estado.Descripcion,
                Estado = estado.Estado
            };

            return View(updateEstado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateEstadoHabitacionViewModel estado)
        {
            if (!ModelState.IsValid)
            {
                return View(estado);
            }

            string apiUrl = $"{_apiUrl}/{estado.IdEstadoHabitacion}";

            try
            {
                var response = await _httpClient.PutAsJsonAsync(apiUrl, estado);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Error al actualizar el estado de la habitación.";
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            return View(estado);
        }

        public async Task<IActionResult> Delete(int id)
        {
            EstadoHabitacionViewModel estado = null;
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<EstadoHabitacionViewModel>>(apiUrl);
                if (response != null)
                {
                    estado = response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EstadoHabitacionViewModel estado)
        {
            string apiUrl = $"{_apiUrl}/{estado.IdEstadoHabitacion}";

            try
            {
                var response = await _httpClient.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Error al eliminar el estado de la habitación.";
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
