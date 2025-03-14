
using MedicalAppointment.Persistence.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Application.DTos.habitacion.Tarifa;
using SGHR.Application.Interfaces.habitacion;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Interfaces.habitacion;

namespace SGHR.Application.Service.habitacion
{
    public class TarifaService : ITarifaService
    {
        private readonly ITarifaRepository _repository;
        private readonly ILogger<TarifaService> _logger;
        private readonly IConfiguration _configuration;

        public TarifaService(ITarifaRepository repository, ILogger<TarifaService> logger, IConfiguration configuration)
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
                var tarifas = await _repository.GetAllAsync(x => !x.Borrado);
                result.Success = true;
                result.Data = tarifas;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorGetAllTarifas"];
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> GeById(int id)
        {
            var result = new OperationResult();
            try
            {
                var tarifa = await _repository.GetEntityByIdAsync(id);
                if (tarifa == null)
                {
                    result.Success = false;
                    result.Message = _configuration["TarifaNotFound"];
                    return result;
                }
                result.Success = true;
                result.Data = tarifa;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorGetTarifaById"];
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> Remove(RemoveTarifaDTO dto)
        {
            var result = new OperationResult();
            try
            {
                var tarifa = await _repository.GetEntityByIdAsync(dto.IdTarifa);
                if (tarifa == null)
                {
                    result.Success = false;
                    result.Message = _configuration["TarifaNotFound"];
                    return result;
                }
                tarifa.Borrado = true;
                tarifa.FechaEliminado = DateTime.UtcNow;
                await _repository.UpdateEntityAsync(tarifa);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorRemoveTarifa"];
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> Save(SaveTarifaDTO dto)
        {
            var result = new OperationResult();
            try
            {
                var tarifa = new Tarifa { };
                var isValid = await BaseValidator<Tarifa>.ValidateEntityAsync(tarifa);
                if (!isValid.Success)
                {
                    return isValid;
                }
                await _repository.SaveEntityAsync(tarifa);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorSaveTarifa"];
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> Update(UpdateTarifaDTO dto)
        {
            var result = new OperationResult();
            try
            {
                var tarifa = await _repository.GetEntityByIdAsync(dto.IdTarifa);
                if (tarifa == null)
                {
                    result.Success = false;
                    result.Message = _configuration["TarifaNotFound"];
                    return result;
                }
                var isValid = await BaseValidator<Tarifa>.ValidateEntityAsync(tarifa);
                if (!isValid.Success)
                {
                    return isValid;
                }
                await _repository.UpdateEntityAsync(tarifa);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorUpdateTarifa"];
                _logger.LogError(ex, result.Message);
            }
            return result;
        }
    }
}
