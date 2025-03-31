using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGHR.Web.Comsuming.Api.MAL.Models.usuario;
using System.Threading.Tasks;

namespace SGHR.Web.Comsuming.Api.MAL.Controllers.usuario
{
    public class ClienteController : Controller
    {
        HttpClient _client;
        string baseurl = "http://localhost:5017/api/";
        string geturl = "Cliente/GetCliente";
        string getidurl = "Cliente/GetClienteById?id=";
        string posturl = "Cliente/SaveCliente";
        string puturl = "Cliente/UpdateCliente";

        public ClienteController()
        {
            _client = new HttpClient();
        }

        // GET: ClienteController
        public async Task<IActionResult> Index()
        {
            List<ClienteModel> clienteList = new List<ClienteModel>();
            try{
                var response = await _client.GetFromJsonAsync<ApiResponse<List<ClienteModel>>>(baseurl + geturl);
                if (response != null && response.Success)
                {
                    clienteList = response.Data;
                }
            } catch (HttpRequestException ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(clienteList);
        }

        // GET: ClienteController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ClienteModel cliente = new ClienteModel();
            try
            {
                var response = await _client.GetFromJsonAsync<ApiResponse<ClienteModel>>(baseurl + getidurl + id);
                if (response != null && response.Success)
                {
                    cliente = response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(cliente);
        }

        // GET: ClienteController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var response = await _client.PostAsJsonAsync<ClienteModel>(baseurl + posturl, model);
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
                return View(model);
            }

            return View(model);
        }

        // GET: ClienteController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ClienteModel cliente = new ClienteModel();
            try
            {
                var response = await _client.GetFromJsonAsync<ApiResponse<ClienteModel>>(baseurl + getidurl + id);
                if (response != null && response.Success)
                {
                    cliente = response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = ex.Message;
            }
            if (cliente == null)
            {
                return RedirectToAction(nameof(Index));
            }
            ClienteModel clienteEditModel = new ClienteModel
            {
                IdCliente = cliente.IdCliente,
                TipoDocumento = cliente.TipoDocumento,
                Documento = cliente.Documento,
                NombreCompleto = cliente.NombreCompleto,
                Correo = cliente.Correo,
                Estado = cliente.Estado,
                FechaCreacion = cliente.FechaCreacion
            };
            return View(clienteEditModel);
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClienteModel model)
        {
            if(id != model.IdCliente)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var response = await _client.PutAsJsonAsync<ClienteModel>(baseurl + puturl, model);
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
