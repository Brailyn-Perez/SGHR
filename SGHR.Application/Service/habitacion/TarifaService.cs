
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Application.DTos.habitacion.Tarifa;
using SGHR.Application.Interfaces.habitacion;
using SGHR.Domain.Base;
using SGHR.Persistence.Interfaces.habitacion;

namespace SGHR.Application.Service.habitacion
{
    public class TarifaService : ITarifaService
    {
        private readonly ITarifaRepository _Repository;
        private readonly ILogger<TarifaService> _logger;
        private readonly IConfiguration _configuration;

        public Task<OperationResult> GeAll()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GeById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Remove(RemoveTarifaDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveTarifaDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateTarifaDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
