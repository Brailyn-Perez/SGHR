using MedicalAppointment.Persistence.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Application.DTos.habitacion.EstadoHabitacion;
using SGHR.Application.Interfaces.habitacion;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Interfaces.habitacion;

namespace SGHR.Application.Service.habitacion
{
    public class EstadoHabitacionService : IEstadoHabitacionService
    {
        private readonly IEstadoHabitacionRepository _repository;
        private readonly ILogger<EstadoHabitacionService> _logger;
        private readonly IConfiguration _configuration;

        public EstadoHabitacionService(IEstadoHabitacionRepository repository, ILogger<EstadoHabitacionService> logger, IConfiguration configuration)
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
                var estados = await _repository.GetAllAsync();
                result.Success = true;
                result.Data = estados.Select(x => new EstadoHabitacionDTO()
                {
                    IdEstadoHabitacion = x.IdEstadoHabitacion,
                    Descripcion = x.Descripcion,
                    Estado = x.Estado
                });
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorGetAllEstadoHabitacion"];
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> GeById(int id)
        {
            var result = new OperationResult();
            try
            {
                var estado = await _repository.GetEntityByIdAsync(id);
                if (estado == null)
                {
                    result.Success = false;
                    result.Message = _configuration["EstadoHabitacionNotFound"];
                    return result;
                }
                result.Success = true;
                result.Data = new EstadoHabitacionDTO()
                {
                    IdEstadoHabitacion = estado.IdEstadoHabitacion,
                    Descripcion = estado.Descripcion,
                    Estado = estado.Estado
                };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorGetEstadoHabitacionById"];
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> Remove(RemoveEstadoHabitacionDTO dto)
        {
            var result = new OperationResult();
            try
            {
                var estado = await _repository.GetEntityByIdAsync(dto.IdEstadoHabitacion);
                if (estado == null)
                {
                    result.Success = false;
                    result.Message = _configuration["EstadoHabitacionNotFound"];
                    return result;
                }

                estado.Borrado = true;
                estado.FechaEliminado = DateTime.UtcNow;

                await _repository.UpdateEntityAsync(estado);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorRemoveEstadoHabitacion"];
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> Save(SaveEstadoHabitacionDTO dto)
        {
            var result = new OperationResult();
            try
            {
                var estado = new EstadoHabitacion
                {
                    Descripcion = dto.Descripcion,
                    Estado = dto.Estado,
                    FechaCreacion = DateTime.UtcNow,
                    UsuarioCreacion = 0
                };

                var isValid = await BaseValidator<EstadoHabitacion>.ValidateEntityAsync(estado);
                if (!isValid.Success)
                {
                    return isValid;
                }

                await _repository.SaveEntityAsync(estado);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorSaveEstadoHabitacion"];
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> Update(UpdateEstadoHabitacionDTO dto)
        {
            var result = new OperationResult();
            try
            {
                var estado = await _repository.GetEntityByIdAsync(dto.IdEstadoHabitacion);
                if (estado == null)
                {
                    result.Success = false;
                    result.Message = _configuration["EstadoHabitacionNotFound"];
                    return result;
                }


                var isValid = await BaseValidator<EstadoHabitacion>.ValidateEntityAsync(estado);
                if (!isValid.Success)
                {
                    return isValid;
                }

                estado.Descripcion = dto.Descripcion;
                estado.Estado = dto.Estado;
                estado.FechaActualizacion = DateTime.UtcNow;
                estado.UsuarioActualizacion = 0;

                await _repository.UpdateEntityAsync(estado);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorUpdateEstadoHabitacion"];
                _logger.LogError(ex, result.Message);
            }
            return result;
        }
    }
}
