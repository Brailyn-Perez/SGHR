using Microsoft.EntityFrameworkCore;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.reserva;
using System;

namespace SGHR.Persistence.Base.ValidateRepository.ValidateReservaHandler
{
    public class ReservaNotNullProperties : IValidatorHandler<Reserva>
    {
        private IValidatorHandler<Reserva>? _next;

        public IValidatorHandler<Reserva>? SetNext(IValidatorHandler<Reserva> next)
        {
            _next = next;
            return _next;
        }

        public OperationResult Handler(Reserva reserva)
        {
            OperationResult result = new();

            // Asignar la entidad al resultado para mantener la referencia
            result.Data = reserva;

            if (reserva.IdCliente < 0)
            {
                result.Success = false;
                result.Message = "EL id del cliente no puede ser menor o igual a 0";
                return result;
            }
            if (reserva.IdHabitacion <= 0)
            {
                result.Success = false;
                result.Message = "EL id de la Habitacion no puede ser menor o igual a 0";
                return result;
            }

            if (reserva.FechaEntrada > reserva.FechaSalida)
            {
                result.Success = false;
                result.Message = "La Fecha de entrada no puede ser porterior a la fecha de salida";
                return result;
            }
            if (reserva.FechaEntrada == DateTime.MinValue)
            {
                result.Success = false;
                result.Message = "fecha de entrada es requerida";
                return result;
            }

            if (reserva.FechaSalida < reserva.FechaEntrada)
            {
                result.Success = false;
                result.Message = "La fecha de salida no puede ser menor a la fecha de entrada";
                return result;
            }
            if (reserva.FechaSalida == DateTime.MinValue)
            {
                result.Success = false;
                result.Message = "fecha de Salida es requerida";
                return result;
            }

            if (reserva.FechaSalidaConfirmacion < reserva.FechaSalida)
            {
                result.Success = false;
                result.Message = "La fecha de confirmacion de salida no puede ser menor a la fecha de entrada";
                return result;
            }
            if (reserva.FechaSalidaConfirmacion == DateTime.MinValue)
            {
                result.Success = false;
                result.Message = "fecha de confirmacion de salida es requerida";
                return result;
            }

            if (reserva.PrecioInicial < 0000000000.00m)
            {
                result.Success = false;
                result.Message = "El precio inicial no puede ser menor a 0";
                return result;
            }
            if (decimal.Round(reserva.PrecioInicial, 2) != reserva.PrecioInicial)
            {
                result.Success = false;
                result.Message = "El precio inicial no puede tener mas de dos decimales";
                return result;
            }

            if (reserva.Adelanto < 0000000000.00m)
            {
                result.Success = false;
                result.Message = "El adelanto no puede ser menor a 0";
                return result;
            }
            if (decimal.Round(reserva.Adelanto, 2) != reserva.Adelanto)
            {
                result.Success = false;
                result.Message = "El adelanto no puede tener mas de dos decimales";
                return result;
            }

            if (reserva.PrecioRestante < 0000000000.00m)
            {
                result.Success = false;
                result.Message = "El PrecioRestante no puede ser menor a 0";
                return result;
            }
            if (decimal.Round(reserva.PrecioRestante, 2) != reserva.PrecioRestante)
            {
                result.Success = false;
                result.Message = "El PrecioRestante no puede tener mas de dos decimales";
                return result;
            }
            if (reserva.PrecioRestante > reserva.PrecioInicial)
            {
                result.Success = false;
                result.Message = "el precio restante no puede ser mayor al precio inicial";
                return result;
            }

            if (reserva.TotalPagado < 0000000000.00m)
            {
                result.Success = false;
                result.Message = "El TotalPagado no puede ser menor a 0";
                return result;
            }
            if (decimal.Round(reserva.TotalPagado, 2) != reserva.TotalPagado)
            {
                result.Success = false;
                result.Message = "El TotalPagado no puede tener mas de dos decimales";
                return result;
            }

            if (reserva.CostoPenalidad < 0000000000.00m)
            {
                result.Success = false;
                result.Message = "El CostoPenalidad no puede ser menor a 0";
                return result;
            }
            if (decimal.Round(reserva.CostoPenalidad, 2) != reserva.CostoPenalidad)
            {
                result.Success = false;
                result.Message = "El CostoPenalidad no puede tener mas de dos decimales";
                return result;
            }

            // Corregí la condición para verificar si la observación es MÁS larga que 500 caracteres
            if (reserva.Observacion.Length > 500)
            {
                result.Success = false;
                result.Message = "La observacion no puede pasar de los 500 caracteres";
                return result;
            }

            if (reserva.NumeroHuespedes <= 0)
            {
                result.Success = false;
                result.Message = "el numero de huspedes es requerio y no puede ser 0 ni menor a 0";
                return result;
            }

            if (reserva.UsuarioCreacion <= 0)
            {
                result.Success = false;
                result.Message = "El usuario Creacion no puede ser menor o igual a 0 y es requerido";
                return result;
            }

            if (reserva.FechaSalidaConfirmacion == DateTime.MinValue)
            {
                result.Success = false;
                result.Message = "fecha de Creacion es requerida";
                return result;
            }

            var nextResult = _next?.Handler(reserva) ?? result;

            // Si el siguiente handler no devolvió una entidad en Data, 
            // asegurarnos de mantener la entidad original
            if (nextResult.Data == null)
            {
                nextResult.Data = reserva;
            }

            return nextResult;
        }
    }
}