using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGHR.Web.Comsuming.Api.MAL.Models.usuario;
using System.Threading.Tasks;

namespace SGHR.Web.Comsuming.Api.MAL.Controllers
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
                        user = (UsuarioModel)apiResponse.Data;
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
        public ActionResult Create(IFormCollection collection)
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

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
