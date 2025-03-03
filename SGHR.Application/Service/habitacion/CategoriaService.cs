using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Application.DTos.habitacion.Categoria;
using SGHR.Application.Interfaces.habitacion;
using SGHR.Domain.Base;
using SGHR.Persistence.Interfaces.habitacion;

namespace SGHR.Application.Service.habitacion
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _Repository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CategoriaService> _logger;

        public CategoriaService(ICategoriaRepository repository, IConfiguration configuration, ILogger<CategoriaService> logger)
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

        public Task<OperationResult> Remove(RemoveCategoriaDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveCategoriaDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateCategoriaDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
