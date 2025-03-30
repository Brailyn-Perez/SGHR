using Microsoft.AspNetCore.Mvc;
using SGHR.WEB.Consumiendo.Models.Base;
using SGHR.WEB.Consumiendo.Models.habitacion.Categoria;
using SGHR.WEB.Consumiendo.Models.habitacion.Habitacion;
namespace SGHR.WEB.Consumiendo.Controllers
{
    public class HabitacionController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "http://localhost:5017/api/Habitacion";

        public HabitacionController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            List<HabitacionViewModel> habitaciones = new();

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<HabitacionViewModel>>>(_apiUrl);
                if (response != null)
                {
                    habitaciones = response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            return View(habitaciones);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            HabitacionViewModel habitacion = null;
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<HabitacionViewModel>>(apiUrl);
                if (response != null && response.Success)
                {
                    habitacion = response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            if (habitacion == null)
            {
                return NotFound();
            }

            return View(habitacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateHabitacionViewModel habitacion)
        {
            if (!ModelState.IsValid)
            {
                return View(habitacion);
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_apiUrl, habitacion);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Error al guardar la habitación.";
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            return View(habitacion);
        }

        public async Task<IActionResult> Edit(int id)
        {
            HabitacionViewModel habitacion = null;
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<HabitacionViewModel>>(apiUrl);
                if (response != null)
                {
                    habitacion = response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            if (habitacion == null)
            {
                return NotFound();
            }

            UpdateHabitacionViewModel updateHabitacion = new()
            {
                IdHabitacion = habitacion.IdHabitacion,
                Numero = habitacion.Numero,
                Detalle = habitacion.Detalle,
                Precio = habitacion.Precio,
                Estado = habitacion.Estado
            };

            return View(updateHabitacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateHabitacionViewModel habitacion)
        {
            if (id != habitacion.IdHabitacion)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(habitacion);
            }

            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.PutAsJsonAsync(apiUrl, habitacion);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Error al actualizar la habitación.";
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            return View(habitacion);
        }

        public async Task<IActionResult> Delete(int id)
        {
            HabitacionViewModel habitacion = null;
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<HabitacionViewModel>>(apiUrl);
                if (response != null)
                {
                    habitacion = response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            if (habitacion == null)
            {
                return NotFound();
            }

            return View(habitacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(HabitacionViewModel habitacionViewModel)
        {
            string apiUrl = $"{_apiUrl}/{habitacionViewModel.IdHabitacion}";

            try
            {
                var response = await _httpClient.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Error al eliminar la habitación.";
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
