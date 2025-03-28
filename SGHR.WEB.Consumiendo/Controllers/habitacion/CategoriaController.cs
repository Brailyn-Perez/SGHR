using Microsoft.AspNetCore.Mvc;
using SGHR.WEB.Consumiendo.Models.Base;
using SGHR.WEB.Consumiendo.Models.habitacion.Categoria;

namespace SGHR.WEB.Consumiendo.Controllers.habitacion
{
    public class CategoriaController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "http://localhost:5017/api/Categoria";

        public CategoriaController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            List<CategoriaViewModel> categorias = new();

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<CategoriaViewModel>>>(_apiUrl);
                if (response != null && response.Success)
                {
                    categorias = response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            return View(categorias);
        }

        public async Task<IActionResult> Details(int id)
        {
            CategoriaViewModel categoria = null;
            string apiUrl = $"http://localhost:5017/api/Categoria/{id}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<CategoriaViewModel>>(apiUrl);
                if (response != null && response.Success)
                {
                    categoria = response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }


        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaViewModel categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_apiUrl, categoria);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Error al guardar la categoría.";
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            return View(categoria);
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
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

        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
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
