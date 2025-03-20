using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Application.DTos.reserva.Reserva;
using SGHR.Application.Interfaces.reserva;
using SGHR.Application.Service.habitacion;
using SGHR.Application.Service.Manejo;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.reserva;
using SGHR.Persistence.Interfaces.habitacion;
using SGHR.Persistence.Interfaces.reserva;
using SGHR.Persistence.Repositories.servicio;
using System.Linq.Expressions;

namespace SGHR.Application.Service.reserva
{
    public class ReservaServise : IReservaService
    {
        private readonly IReservaRepository _Repository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<IReservaService> _logger;

        public ReservaServise(IReservaRepository repository, IConfiguration configuration, ILogger<IReservaService> logger)
        {
            _Repository = repository;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<OperationResult> GeAll()
        {
            OperationResult result = new();
            try
            {
                result.Data = await _Repository.GetAllAsync();
                result.Message = _configuration["TodoOk:getAllAsync"];
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["Error:getAllAsync"];
            }
            return result;
        }


        public async Task<OperationResult> GeById(int id)
        {
            OperationResult result = new();
            try{
                result.Data = await _Repository.GetEntityByIdAsync(id);
                result.Message = "Entidad obtenida Correctamente";
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorAlObtenerLaEntidad:GeById"];
            }
            return result;
        }

        public async Task<OperationResult> Remove(RemoveReservaDTO dto)
        {
            OperationResult result = new();
            try
            {
                var entity = await _Repository.GetEntityByIdAsync(dto.IdReserva);
                if (entity == null)
                {
                    result.Success = false;
                    result.Message = "Reserva no encontrada.";
                    return result;
                }

                entity.Borrado = true;
                await _Repository.UpdateEntityAsync(entity);
                result.Data = entity;
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorAlRemoverLaEntidad:Remove"];
                _logger.LogError(result.Message, result.Success);
            }
            return result;
        }

        public async Task<OperationResult> Save(SaveReservaDTO dto)
        {
            OperationResult result = new();
            try
            {
                result.Data = await _Repository.SaveEntityAsync(new Reserva()
                {
                    IdCliente = dto.IdCliente,
                    IdHabitacion = dto.IdHabitacion,
                    FechaEntrada = dto.FechaEntrada,
                    FechaSalida = dto.FechaSalida,
                    PrecioInicial = dto.PrecioInicial,
                    Adelanto = dto.Adelanto,
                    Observacion = dto.Observacion,
                    NumeroHuespedes = dto.NumeroHuespedes
                });
                result.Message = "reserva guardada";
            }
            catch(Exception ex)
            {
                result.Success = false;
            }
            return result;
        }

        public async Task<OperationResult> Update(UpdateReservaDTO dto)
        {
            OperationResult result = new();
            try
            {
                var entity = await _Repository.GetEntityByIdAsync(dto.IdReserva);

                if (entity == null)
                {
                    result.Success = false;
                    result.Message = "Reserva no encontrada.";
                    return result;
                }

                entity.FechaSalidaConfirmacion = dto.FechaSalidaConfirmacion;
                entity.PrecioRestante = dto.PrecioRestante;
                entity.TotalPagado = dto.TotalPagado;
                entity.CostoPenalidad = dto.CostoPenalidad;
                entity.Estado = dto.Estado;
                entity.IdCliente = dto.IdCliente;
                entity.IdHabitacion = dto.IdHabitacion;
                entity.FechaEntrada = dto.FechaEntrada;
                entity.FechaSalida = dto.FechaSalida;
                entity.PrecioInicial = dto.PrecioInicial;
                entity.Adelanto = dto.Adelanto;
                entity.Observacion = dto.Observacion;
                entity.NumeroHuespedes = dto.NumeroHuespedes;

                var updateResult = await _Repository.UpdateEntityAsync(entity);

                if (updateResult.Success)
                {
                    result.Success = true;
                    result.Message = "Reserva actualizada exitosamente.";
                    result.Data = entity;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Error al actualizar la reserva.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error: {ex.Message}";
            }

            return result;
        }
    }
}
