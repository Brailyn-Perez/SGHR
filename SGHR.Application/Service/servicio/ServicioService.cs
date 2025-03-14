

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Application.DTos.servicio.Servicio;
using SGHR.Application.Interfaces.sevicio;
using SGHR.Application.Service.habitacion;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.servicio;
using SGHR.Persistence.Interfaces.habitacion;
using SGHR.Persistence.Interfaces.servicio;


namespace SGHR.Application.Service.servicio
{
    public class ServicioService : IServiciosService
    {
        private readonly IServicioRepository _Repository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ServiciosRespository> _logger;

        public ServicioService(IServicioRepository repository, IConfiguration configuration, ILogger<ServiciosRespository> logger)
        {
            _Repository = repository;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<OperationResult> GeAll()
        {
            OperationResult result = new();
            try{
                result.Data = await _Repository.GetAllAsync();
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorOcteniendoLosService:GetAll"];
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async Task<OperationResult> GeById(int id)
        {
            OperationResult result = new();
            try
            {
                result.Data = await _Repository.GetEntityByIdAsync(id);
            }catch(Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorOcteniendoElServicePorID:GetEntityByIdAsync3"];
            }
            return result;
        }

        public async Task<OperationResult> Save(SaveServicioDTO dto)
        {
            OperationResult result = new();
            result.Data = await _Repository.SaveEntityAsync(new Servicios()
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            });
            return result;
        }

        public async Task<OperationResult> Update(UpdateServicioDTO dto)
        {
            OperationResult result = new();
            var entity = await _Repository.GetEntityByIdAsync(dto.IdServicio);
            result.Data = await _Repository.UpdateEntityAsync(entity = new Servicios()
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            });
            return result;
        }

        public Task<OperationResult> Remove(RemoveServicioDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
