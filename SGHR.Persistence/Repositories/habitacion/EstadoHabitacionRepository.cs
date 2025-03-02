
using MedicalAppointment.Persistence.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Base;
using SGHR.Persistence.Context;
using SGHR.Persistence.Interfaces.habitacion;
using System.Linq.Expressions;

namespace SGHR.Persistence.Repositories.habitacion
{
    public class EstadoHabitacionRepository : BaseRepository<EstadoHabitacion> , IEstadoHabitacionRepository
    {
        private readonly SGHRContext _context;
        private readonly ILogger<EstadoHabitacionRepository> _logger;
        private readonly IConfiguration _configuration;

        public EstadoHabitacionRepository(SGHRContext context, ILogger<EstadoHabitacionRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public override Task<OperationResult> GetAllAsync(Expression<Func<EstadoHabitacion, bool>> filter)
        {
            return base.GetAllAsync(filter);
        }

        public override async Task<EstadoHabitacion> GetEntityByIdAsync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var isValid = await BaseValidator<EstadoHabitacion>.ValidateID(id);
                if (!isValid.Success)
                {
                    throw new Exception("El id es invalido");
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorGetEntityAsync"];
                _logger.LogError(ex.Message, result.Message);
            }

            return await base.GetEntityByIdAsync(id);
        }

        public override async Task<OperationResult> SaveEntityAsync(EstadoHabitacion entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                var isValid = await BaseValidator<EstadoHabitacion>.ValidateEntityAsync(entity);
                if (!isValid.Success)
                {
                    return isValid;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorGetEntityAsync"];
                _logger.LogError(ex.Message, result.Message);
            }
            return await base.SaveEntityAsync(entity);
        }

        public override async Task<OperationResult> UpdateEntityAsync(EstadoHabitacion entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                var isValid = await BaseValidator<EstadoHabitacion>.ValidateEntityAsync(entity);
                if (!isValid.Success)
                {
                    return isValid;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorGetEntityAsync"];
                _logger.LogError(ex.Message, result.Message);
            }

            return await base.UpdateEntityAsync(entity);
        }
    }
}
