using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGHR.Application.DTos.habitacion.Categoria;
using SGHR.Application.DTos.reserva.Reserva;
using SGHR.Application.Interfaces.reserva;
using System.Threading.Tasks;

namespace SGHR.Web.Controllers.reserva
{
    public class ReservaController : Controller
    {
        private readonly IReservaService _reservaServise; 

        public ReservaController(IReservaService reservaService)
        {
            _reservaServise = reservaService;
        }

        // GET: ReservaController
        public async Task<IActionResult> Index()
        {
            var reserva = await _reservaServise.GeAll();
            return View(reserva.Data);
        }

        // GET: ReservaController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var categoria = await _reservaServise.GeById(id);            
            return View(categoria.Data);
        }

        // GET: ReservaController/Create
        public  ActionResult Create()
        {
            var saveReservaDto = new SaveReservaDTO();
            return View(saveReservaDto);
        }

        // POST: ReservaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SaveReservaDTO saveReservaDto)
        {
            try
            {
                await _reservaServise.Save(saveReservaDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservaController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _reservaServise.GeById(id);
            var reserva = result.Data;
            UpdateReservaDTO updatereserva = new UpdateReservaDTO()
            {
                IdReserva = reserva.IdReserva,
                IdCliente = reserva.IdCliente,
                IdHabitacion = reserva.IdHabitacion,
                FechaEntrada = reserva.FechaEntrada,
                FechaSalida = reserva.FechaSalida,
                PrecioInicial = reserva.PrecioInicial,
                Adelanto = reserva.Adelanto,
                Observacion = reserva.Observacion,
                NumeroHuespedes = reserva.NumeroHuespedes,
                FechaSalidaConfirmacion = reserva.FechaSalidaConfirmacion,
                PrecioRestante = reserva.PrecioRestante,
                TotalPagado = reserva.TotalPagado,
                CostoPenalidad = reserva.CostoPenalidad,
                Estado = reserva.Estado,

            };
            return View(updatereserva);
        }

        // POST: ReservaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id,  UpdateReservaDTO reserva)
        {
            try
            {
                await _reservaServise.Update(reserva);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
