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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoriaViewModel categoria)
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

            EditCategoriaViewModel editCategoriaViewModel = new()
            {
                IdCategoria = id,
                Descripcion = categoria.Descripcion,
                Estado = categoria.Estado
            };

            return View(editCategoriaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditCategoriaViewModel categoria)
        {
            if (id != categoria.IdCategoria)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(categoria);
            }

            string apiUrl = $"http://localhost:5017/api/Categoria/{id}";

            try
            {
                var response = await _httpClient.PutAsJsonAsync(apiUrl, categoria);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Error al actualizar la categoría.";
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            return View(categoria);
        }


        public async Task<IActionResult> Delete(int id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string apiUrl = $"http://localhost:5017/api/Categoria/{id}";

            try
            {
                var response = await _httpClient.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Error al eliminar la categoría.";
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
