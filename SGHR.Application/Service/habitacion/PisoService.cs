
using MedicalAppointment.Persistence.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Application.DTos.habitacion.Piso;
using SGHR.Application.Interfaces.habitacion;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Interfaces.habitacion;

namespace SGHR.Application.Service.habitacion
{
    public class PisoService : IPisoService
    {
        private readonly IPisoRepository _repository;
        private readonly ILogger<PisoService> _logger;
        private readonly IConfiguration _configuration;

        public PisoService(IPisoRepository repository, ILogger<PisoService> logger, IConfiguration configuration)
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
                var pisos = await _repository.GetAllAsync();
                result.Success = true;
                result.Data = pisos.Select(x => new PisoDTO()
                {
                    IdPiso = x.IdPiso,
                    Descripcion = x.Descripcion,
                    Estado = x.Estado
                });
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorGetAllPisos"];
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> GeById(int id)
        {
            var result = new OperationResult();
            try
            {
                var piso = await _repository.GetEntityByIdAsync(id);
                if (piso == null)
                {
                    result.Success = false;
                    result.Message = _configuration["PisoNotFound"];
                    return result;
                }
                result.Success = true;
                result.Data = new PisoDTO()
                {
                    IdPiso = piso.IdPiso,
                    Descripcion = piso.Descripcion,
                    Estado = piso.Estado
                };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorGetPisoById"];
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> Remove(RemovePisoDTO dto)
        {
            var result = new OperationResult();
            try
            {
                var piso = await _repository.GetEntityByIdAsync(dto.IdPiso);
                if (piso == null)
                {
                    result.Success = false;
                    result.Message = _configuration["PisoNotFound"];
                    return result;
                }
                piso.Borrado = true;
                piso.FechaEliminado = DateTime.UtcNow;
                await _repository.UpdateEntityAsync(piso);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorRemovePiso"];
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> Save(SavePisoDTO dto)
        {
            var result = new OperationResult();
            try
            {
                var piso = new Piso { };
                var isValid = await BaseValidator<Piso>.ValidateEntityAsync(piso);
                if (!isValid.Success)
                {
                    return isValid;
                }
                await _repository.SaveEntityAsync(piso);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorSavePiso"];
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> Update(UpdatePisoDTO dto)
        {
            var result = new OperationResult();
            try
            {
                var piso = await _repository.GetEntityByIdAsync(dto.IdPiso);
                if (piso == null)
                {
                    result.Success = false;
                    result.Message = _configuration["PisoNotFound"];
                    return result;
                }
                var isValid = await BaseValidator<Piso>.ValidateEntityAsync(piso);
                if (!isValid.Success)
                {
                    return isValid;
                }
                await _repository.UpdateEntityAsync(piso);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorUpdatePiso"];
                _logger.LogError(ex, result.Message);
            }
            return result;
        }
    }
}
