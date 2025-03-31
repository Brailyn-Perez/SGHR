using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGHR.Web.Comsuming.Api.MAL.Models.reserva;

namespace SGHR.Web.Comsuming.Api.MAL.Controllers.reserva
{
    public class ReservaController : Controller
    {
        HttpClient _client;
        string baseurl = "http://localhost:5017/api";
        string geturl = "/Reserva";
        string getidurl = "/Reserva/"; // + id
        string posturl = "/Reserva";
        string puturl = "/Reserva/"; // + id
        string deleteurl = "/Reserva/"; // + id

        public ReservaController(HttpClient client)
        {
            _client = client;
        }
        // GET: ReservaController
        public async Task<ActionResult> Index()
        {
            List<ReservaModel> reservas = new();
            try
            {
                var response = await _client.GetFromJsonAsync<ApiResponse<List<ReservaModel>>>(baseurl + geturl);
                if (response != null && response.Success)
                {
                    reservas = response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(reservas);
        }

        // GET: ReservaController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ReservaModel reserva = new();
            try
            {
                var response = await _client.GetFromJsonAsync<ApiResponse<ReservaModel>>(baseurl + getidurl + id);
                if (response != null && response.Success)
                {
                    reserva = response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(reserva);
        }

        // GET: ReservaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReservaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservaModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var response = await _client.PostAsJsonAsync<ReservaModel>(baseurl + posturl, model);
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

        // GET: ReservaController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ReservaModel reserva = new();
            try
            {
                var response = await _client.GetFromJsonAsync<ApiResponse<ReservaModel>>(baseurl + getidurl + id);
                if (response != null && response.Success)
                {
                    reserva = response.Data;
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = ex.Message;
            }
            if(reserva == null)
            {
                return RedirectToAction(nameof(Index));
            }
            ReservaModel editReservaModel = new()
            {
                IdReserva = reserva.IdReserva,
                IdCliente = reserva.IdCliente,
                IdHabitacion = reserva.IdHabitacion,
                FechaEntrada = reserva.FechaEntrada,
                FechaSalida = reserva.FechaSalida,
                FechaSalidaConfirmacion = reserva.FechaSalidaConfirmacion,
                PrecioInicial = reserva.PrecioInicial,
                Adelanto = reserva.Adelanto,
                PrecioRestante = reserva.PrecioRestante,
                TotalPagado = reserva.TotalPagado,
                CostoPenalidad = reserva.CostoPenalidad,
                Observacion = reserva.Observacion,
                NumeroHuespedes = reserva.NumeroHuespedes,
                Estado = reserva.Estado
            };
            return View(editReservaModel);
        }

        // POST: ReservaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ReservaModel model)
        {
            if(id != model.IdReserva)
            {
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var response = await _client.PutAsJsonAsync<ReservaModel>(baseurl + puturl, model);
                if(response.IsSuccessStatusCode) 
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = response.ReasonPhrase;
                    return View(model);
                }
            }catch (HttpRequestException ex)
            {
                ViewBag["error"] = ex.Message;
            }
            return View(model);
        }

        // GET: ReservaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReservaController/Delete/5
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
