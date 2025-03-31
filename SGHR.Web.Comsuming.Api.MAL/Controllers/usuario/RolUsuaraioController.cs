using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGHR.Web.Comsuming.Api.MAL.Models.usuario;
using System.Threading.Tasks;
/*
 http://localhost:5017/api/RolUsuario/GetRolUsuario
 http://localhost:5017/api/RolUsuario/GetRolUsuarioById?id=1
 http://localhost:5017/api/RolUsuario/SaveRolUsuario
 http://localhost:5017/api/RolUsuario/UpdateRolUsuario
 */
namespace SGHR.Web.Comsuming.Api.MAL.Controllers.usuario
{
    public class RolUsuaraioController : Controller
    {
        HttpClient _client;
        static string url = "https://localhost:44357/api/RolUsuario";
        static string geturi = "/GetRolUsuario";
        static string getbyiduri = "/GetRolUsuarioById?id=";
        static string posturi = "/SaveRolUsuario";
        static string puturi = "/UpdateRolUsuario";

        public RolUsuaraioController(HttpClient client)
        {
            this._client = client;
        }

        // GET: RolUsuaraioController
        public async Task<ActionResult> Index()
        {
            List<RolUsuarioModel> Roles = new();
            try
            {
                var response = await _client.GetFromJsonAsync<ApiResponse<List<RolUsuarioModel>>>(url + geturi);
                if (response != null && response.Success)
                {
                    Roles = response.Data;
                }
            }catch (HttpRequestException ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(Roles);
        }

        // GET: RolUsuaraioController/Details/5
        public IActionResult Details(int id)
        {
            RolUsuarioModel rol = new();
            try
            {
                var response = _client.GetFromJsonAsync<ApiResponse<RolUsuarioModel>>(url + getbyiduri + id).Result;
                if (response != null && response.Success)
                {
                    rol = response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        // GET: RolUsuaraioController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RolUsuaraioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RolUsuarioModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var response = await _client.PostAsJsonAsync<RolUsuarioModel>(url + posturi, model);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = response.ReasonPhrase;
                    return View(model);
                }
            }
            catch(HttpRequestException ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }

        // GET: RolUsuaraioController/Edit/5
        public IActionResult Edit(int id)
        {
            RolUsuarioModel rol = new();
            try
            {
                var response = _client.GetFromJsonAsync<ApiResponse<RolUsuarioModel>>(url + getbyiduri + id).Result;
                if (response != null && response.Success)
                {
                    rol = response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = ex.Message;
            }
            if (rol == null)
            {
                return RedirectToAction(nameof(Index));
            }
            RolUsuarioModel rolEditmodel = new()
            {
                IdRolUsuario = rol.IdRolUsuario,
                Descripcion = rol.Descripcion,
                Estado = rol.Estado,
                FechaCreacion = rol.FechaCreacion
            };
            return View(rolEditmodel);
        }

        // POST: RolUsuaraioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RolUsuarioModel model)
        {
            if(id != model.IdRolUsuario)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var response = await _client.PutAsJsonAsync<RolUsuarioModel>(url + puturi, model);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = response.ReasonPhrase;
                    return View(model);
                }
            }
            catch(HttpRequestException ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }
    }
}
