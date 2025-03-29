using Microsoft.AspNetCore.Mvc;
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

        // GET: Index
        public async Task<IActionResult> Index()
        {
            List<HabitacionViewModel> habitaciones = new();

            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<HabitacionViewModel>>(_apiUrl);
                if (response != null)
                {
                    habitaciones = response;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            return View(habitaciones);
        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HabitacionViewModel habitacion)
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

        // GET: Edit
        public async Task<IActionResult> Edit(int id)
        {
            HabitacionViewModel habitacion = null;
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<HabitacionViewModel>(apiUrl);
                if (response != null)
                {
                    habitacion = response;
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

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HabitacionViewModel habitacion)
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

        // GET: Delete
        public async Task<IActionResult> Delete(int id)
        {
            HabitacionViewModel habitacion = null;
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<HabitacionViewModel>(apiUrl);
                if (response != null)
                {
                    habitacion = response;
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
