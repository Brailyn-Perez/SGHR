using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGHR.Web.Comsuming.Api.MAL.Models.usuario;
using System.Net.Http;
using System.Threading.Tasks;

namespace SGHR.Web.Comsuming.Api.MAL.Controllers.usuario
{
    public class UsuarioController : Controller
    {
        // GET: UsuarioController
        public async Task<ActionResult> Index()
        {
            List<UsuarioModel> users = new();
            using (HttpClient client = new())
            {
                client.BaseAddress = new Uri("http://localhost:5017/api/");
                var response = await client.GetAsync("Usuario/GetUsuario");
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<UsuarioModel>>>();
                    if (apiResponse != null && apiResponse.Success)
                    {
                        users = apiResponse.Data;
                    }
                }
            }
            return View(users);
        }

        // GET: UsuarioController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            UsuarioModel user = new();
            using(HttpClient client = new())
            {
                client.BaseAddress = new Uri("http://localhost:5017/api/");
                var response = await client.GetAsync($"/Usuario/GetUsuarioById?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<UsuarioModel>>();
                    if(apiResponse != null && apiResponse.Success)
                    {
                        user = apiResponse.Data;
                    }
                }
            }
            return View(user);
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            UsuarioModel user = new();

            return View(user);
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UsuarioModel Model)
        {
            HttpClient cliente = new();
            if (!ModelState.IsValid)
            {
                return View(Model);
            }
            cliente.BaseAddress = new Uri("http://localhost:5017/api/");
            try
            {
                var response = await cliente.PostAsJsonAsync("Usuario/SaveUsuario", Model);

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

            return View(Model);
        }

        // GET: UsuarioController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            UsuarioModel usuario = null;
            HttpClient client = new();
            client.BaseAddress = new Uri("http://localhost:5017/api/");
            try
            {
                var response = await client.GetFromJsonAsync<ApiResponse<UsuarioModel>>($"/Usuario/GetUsuarioById?id={id}");
                if (response != null && response.Success)
                {
                    usuario = response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            if (usuario == null)
            {
                return NotFound();
            }

            UsuarioModel editUsuarioViewModel = new()
            {
                IdUsuario = id,
                NombreCompleto = usuario.NombreCompleto,
                Correo = usuario.Correo,
                Clave = usuario.Clave,
                Estado = usuario.Estado
            };

            return View(editUsuarioViewModel);
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UsuarioModel usuario)
        {
            if(id != usuario.IdUsuario)
            {
                return BadRequest();
            }
            if(!ModelState.IsValid)
            {
                return View(usuario);
            }
            try
            {
                HttpClient client = new();
                client.BaseAddress = new Uri("http://localhost:5017/api/");
                var response = await client.PutAsJsonAsync("Usuario/UpdateUsuario", usuario);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Error al actualizar el usuario.";
                }
            }
            catch(HttpRequestException ex)
            {
                ViewBag.Error = "erro al conectar con la Api: " + ex.Message;
            }
            return View(usuario);
        }
    }
}
