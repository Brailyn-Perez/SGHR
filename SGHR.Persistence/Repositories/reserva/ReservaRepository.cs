﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.reserva;
using SGHR.Persistence.Base;
using SGHR.Persistence.Context;
using SGHR.Persistence.Interfaces.reserva;


namespace SGHR.Persistence.Repositories.reserva
{
    public class ReservaRepository : BaseRepository<Reserva>, IReservaRepository
    {
        private readonly SGHRContext _context;
        private readonly ILogger<ReservaRepository> _logger;
        private readonly IConfiguration _configuration;

        public ReservaRepository(SGHRContext context, ILogger<ReservaRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> RealizarReservaAsync(Reserva reserva)
        {
            var result = new OperationResult();

            if (reserva.FechaEntrada < DateTime.Now || reserva.FechaSalida < DateTime.Now)
            {
                result.Success = false;
                result.Message = "Las fechas seleccionadas no pueden ser anteriores a la fecha actual.";
                return result;
            }

            var habitacionDisponible = await _context.Habitaciones
                .AnyAsync(h => h.IdHabitacion == reserva.IdHabitacion && h.Estado == true);

            if (!habitacionDisponible)
            {
                result.Success = false;
                result.Message = "La habitación seleccionada no está disponible.";
                return result;
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.IdCategoria == reserva.Habitacion.IdCategoria);

            //if (reserva.NumeroHuespedes > 5) // esto esta mal se modificara luego deve estar comparado con un valor real que viene siendo la cantidad maxima permitido por categoria
            //{
            //    result.Success = false;
            //    result.Message = "El número de huéspedes excede la capacidad máxima de la categoría.";
            //    return result;
            //}


            try
            {
                await SaveEntityAsync(reserva);
                result.Success = true;
                result.Message = "Reserva realizada con éxito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al realizar la reserva.";
                _logger.LogError(ex, "Error al realizar la reserva.");
            }

            return result;
        }

        public async Task<OperationResult> CancelarReservaAsync(int idReserva)
        {
            var result = new OperationResult();

            var reserva = await GetEntityByIdAsync(idReserva);
            if (reserva == null)
            {
                result.Success = false;
                result.Message = "Reserva no encontrada.";
                return result;
            }

            if (reserva.FechaEntrada < DateTime.Now.AddDays(1))
            {
                result.Success = false;
                result.Message = "No se puede cancelar la reserva dentro de las 24 horas previas al check-in.";
                return result;
            }

            reserva.Estado = false;

            try
            {
                await UpdateEntityAsync(reserva);
                result.Success = true;
                result.Message = "Reserva cancelada con éxito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al cancelar la reserva.";
                _logger.LogError(ex, "Error al cancelar la reserva.");
            }

            return result;
        }
    }
}