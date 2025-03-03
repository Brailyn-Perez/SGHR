
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Application.Service.habitacion;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.reserva;
using SGHR.Persistence.Interfaces.habitacion;
using SGHR.Persistence.Interfaces.reserva;
using System.Linq.Expressions;

namespace SGHR.Application.Service.reserva
{
    public class RerervaServise : IReservaRepository
    {
        private readonly ICategoriaRepository _Repository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CategoriaService> _logger;

        public RerervaServise(ICategoriaRepository repository, IConfiguration configuration, ILogger<CategoriaService> logger)
        {
            _Repository = repository;
            _configuration = configuration;
            _logger = logger;
        }

        public Task<OperationResult> CancelarReservaAsync(int idReserva)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Expression<Func<Reserva, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<List<Reserva>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetAllAsync(Expression<Func<Reserva, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<Reserva> GetEntityByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> RealizarReservaAsync(Reserva reserva)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> SaveEntityAsync(Reserva entity)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> UpdateEntityAsync(Reserva entity)
        {
            throw new NotImplementedException();
        }
    }
}
