using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Application.DTos.habitacion.EstadoHabitacion;
using SGHR.Application.Interfaces.habitacion;
using SGHR.Domain.Base;
using SGHR.Persistence.Interfaces.habitacion;

namespace SGHR.Application.Service.habitacion
{
    public class EstadoHabitacionService : IEstadoHabitacionService
    {
        private readonly IEstadoHabitacionRepository _Repository;
        private readonly ILogger<EstadoHabitacionService> _logger;
        private readonly IConfiguration _configuration;

        public EstadoHabitacionService(IEstadoHabitacionRepository repository, ILogger<EstadoHabitacionService> logger, IConfiguration configuration)
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

        public Task<OperationResult> Remove(RemoveEstadoHabitacionDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveEstadoHabitacionDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateEstadoHabitacionDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
