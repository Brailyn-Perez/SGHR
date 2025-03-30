using Microsoft.AspNetCore.Mvc;
using SGHR.WEB.Consumiendo.Models.Base;
using SGHR.WEB.Consumiendo.Models.habitacion.EstadoHabitacion;
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

        public async Task<IActionResult> Index()
        {
            List<TarifaViewModel> tarifas = new();

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<TarifaViewModel>>>(_apiUrl);
                if (response != null)
                {
                    tarifas = response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            return View(tarifas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTarifaViewModel tarifa)
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

        public async Task<IActionResult> Details(int id)
        {
            TarifaViewModel tarifa = null;
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<TarifaViewModel>>(apiUrl);
                if (response != null && response.Success)
                {
                    tarifa = response.Data;
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

        public async Task<IActionResult> Edit(int id)
        {
            TarifaViewModel tarifa = null;
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<TarifaViewModel>>(apiUrl);
                if (response != null)
                {
                    tarifa = response.Data;
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

            TarifaViewModel GetTarifa = null;
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<TarifaViewModel>>(apiUrl);
                if (response != null)
                {
                    GetTarifa = response.Data;
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

            GetTarifa.FechaInicio = tarifa.FechaInicio;
            GetTarifa.FechaFin = tarifa.FechaFin;
            GetTarifa.PrecioPorNoche = tarifa.PrecioPorNoche;
            GetTarifa.Descuento = tarifa.Descuento;
            GetTarifa.Descripcion = tarifa.Descripcion;
            GetTarifa.Estado = tarifa.Estado;

            try
            {
                var response = await _httpClient.PutAsJsonAsync(apiUrl, GetTarifa);

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


        public async Task<IActionResult> Delete(int id)
        {
            TarifaViewModel tarifa = null;
            string apiUrl = $"{_apiUrl}/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<TarifaViewModel>>(apiUrl);
                if (response != null)
                {
                    tarifa = response.Data;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TarifaViewModel tarifa)
        {
            string apiUrl = $"{_apiUrl}/{tarifa.IdTarifa}";

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
