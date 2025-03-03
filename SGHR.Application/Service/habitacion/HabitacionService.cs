
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Application.DTos.habitacion.Habitacion;
using SGHR.Application.Interfaces.habitacion;
using SGHR.Domain.Base;
using SGHR.Persistence.Interfaces.habitacion;

namespace SGHR.Application.Service.habitacion
{
    public class HabitacionService : IHabitacionService
    {
        private readonly IHabitacionRepository _Repository;
        private readonly ILogger<HabitacionService> _logger;
        private readonly IConfiguration _configuration;

        public HabitacionService(IHabitacionRepository repository, ILogger<HabitacionService> logger, IConfiguration configuration)
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

        public Task<OperationResult> Remove(RemoveHabitacionDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveHabitacionDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateHabitacionDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
