using Microsoft.AspNetCore.Mvc;
using SGHR.WEB.Consumiendo.Models.Base;
using SGHR.WEB.Consumiendo.Models.habitacion.EstadoHabitacion;

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

        // GET: Index
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

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEstadoHabitacionViewModel estado)
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

        // GET: Edit
        public async Task<IActionResult> Edit(int id)
        {
            EstadoHabitacionViewModel estado = null;
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<EstadoHabitacionViewModel>(apiUrl);
                if (response != null)
                {
                    estado = response;
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

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EstadoHabitacionViewModel estado)
        {
            if (id != estado.IdEstadoHabitacion)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(estado);
            }

            string apiUrl = $"{_apiUrl}/{id}";

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

        // GET: Delete
        public async Task<IActionResult> Delete(int id)
        {
            EstadoHabitacionViewModel estado = null;
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<EstadoHabitacionViewModel>(apiUrl);
                if (response != null)
                {
                    estado = response;
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
