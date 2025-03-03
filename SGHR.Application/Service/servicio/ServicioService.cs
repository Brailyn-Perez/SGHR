

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Application.DTos.servicio.Servicio;
using SGHR.Application.Interfaces.sevicio;
using SGHR.Application.Service.habitacion;
using SGHR.Domain.Base;
using SGHR.Persistence.Interfaces.habitacion;

namespace SGHR.Application.Service.servicio
{
    public class ServicioService : IServiciosService
    {
        private readonly ICategoriaRepository _Repository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CategoriaService> _logger;

        public ServicioService(ICategoriaRepository repository, IConfiguration configuration, ILogger<CategoriaService> logger)
        {
            _Repository = repository;
            _configuration = configuration;
            _logger = logger;
        }

        public Task<OperationResult> GeAll()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GeById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Remove(RemoveServicioDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveServicioDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateServicioDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
