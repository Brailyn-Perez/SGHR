﻿using MedicalAppointment.Persistence.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Application.DTos.habitacion.Habitacion;
using SGHR.Application.Interfaces.habitacion;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Interfaces.habitacion;

namespace SGHR.Application.Service.habitacion
{
    public class HabitacionService : IHabitacionService
    {
        private readonly IHabitacionRepository _repository;
        private readonly ILogger<HabitacionService> _logger;
        private readonly IConfiguration _configuration;

        public HabitacionService(IHabitacionRepository repository, ILogger<HabitacionService> logger, IConfiguration configuration)
        {
            _repository = repository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> GeAll()
        {
            var result = new OperationResult();
            try
            {
                var habitaciones = await _repository.GetAllAsync();
                result.Success = true;
                result.Data = habitaciones.Select(x => new HabitacionDTO()
                {
                    IdHabitacion = x.IdHabitacion,
                    Numero = x.Numero,
                    Detalle = x.Detalle,
                    Precio = x.Precio,
                    Estado = x.Estado,
                });
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorGetAllHabitaciones"];
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> GeById(int id)
        {
            var result = new OperationResult();
            try
            {
                var habitacion = await _repository.GetEntityByIdAsync(id);
                if (habitacion == null)
                {
                    result.Success = false;
                    result.Message = _configuration["HabitacionNotFound"];
                    return result;
                }
                result.Success = true;
                result.Data = new HabitacionDTO() 
                {
                    IdHabitacion = habitacion.IdHabitacion,
                    Numero = habitacion.Numero,
                    Detalle = habitacion.Detalle,
                    Precio = habitacion.Precio,
                    Estado = habitacion.Estado,
                };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorGetHabitacionById"];
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> Remove(RemoveHabitacionDTO dto)
        {
            var result = new OperationResult();
            try
            {
                var habitacion = await _repository.GetEntityByIdAsync(dto.IdHabitacion);
                if (habitacion == null)
                {
                    result.Success = false;
                    result.Message = _configuration["HabitacionNotFound"];
                    return result;
                }

                habitacion.Borrado = true;
                habitacion.FechaEliminado = DateTime.UtcNow;

                await _repository.UpdateEntityAsync(habitacion);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorRemoveHabitacion"];
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> Save(SaveHabitacionDTO dto)
        {
            var result = new OperationResult();
            try
            {
                var habitacion = new Habitacion
                {
                    Numero = dto.Numero,
                    Detalle = dto.Detalle,
                    Precio = dto.Precio,
                    Estado = dto.Estado,
                    FechaCreacion = DateTime.UtcNow,
                    UsuarioCreacion = 0,
                };

                var isValid = await BaseValidator<Habitacion>.ValidateEntityAsync(habitacion);
                if (!isValid.Success)
                {
                    return isValid;
                }

                await _repository.SaveEntityAsync(habitacion);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorSaveHabitacion"];
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> Update(UpdateHabitacionDTO dto)
        {
            var result = new OperationResult();
            try
            {
                var habitacion = await _repository.GetEntityByIdAsync(dto.IdHabitacion);
                if (habitacion == null)
                {
                    result.Success = false;
                    result.Message = _configuration["HabitacionNotFound"];
                    return result;
                }

                habitacion.Numero = dto.Numero;

                var isValid = await BaseValidator<Habitacion>.ValidateEntityAsync(habitacion);
                if (!isValid.Success)
                {
                    return isValid;
                }

                habitacion.Detalle = dto.Detalle;
                habitacion.Precio = dto.Precio;
                habitacion.Estado = dto.Estado;
                habitacion.Numero = dto.Numero;
                habitacion.FechaActualizacion = DateTime.UtcNow;
                habitacion.UsuarioActualizacion = 0;

                await _repository.UpdateEntityAsync(habitacion);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorUpdateHabitacion"];
                _logger.LogError(ex, result.Message);
            }
            return result;
        }
    }
}
