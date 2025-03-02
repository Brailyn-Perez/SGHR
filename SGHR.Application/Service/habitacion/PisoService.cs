
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Application.DTos.habitacion.Piso;
using SGHR.Application.Interfaces.habitacion;
using SGHR.Domain.Base;
using SGHR.Persistence.Interfaces.habitacion;

namespace SGHR.Application.Service.habitacion
{
    public class PisoService : IPisoService
    {
        private readonly IPisoRepository _Repository;
        private readonly ILogger<PisoService> _logger;
        private readonly IConfiguration _configuration;

        public PisoService(IPisoRepository repository, ILogger<PisoService> logger, IConfiguration configuration)
        {
            _Repository = repository;
            _logger = logger;
            _configuration = configuration;
        }

        public Task<OperationResult> GeAll()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GeById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Remove(RemovePisoDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SavePisoDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdatePisoDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
