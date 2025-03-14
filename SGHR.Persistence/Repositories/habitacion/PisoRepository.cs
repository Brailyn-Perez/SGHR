using MedicalAppointment.Persistence.Base;
using Microsoft.EntityFrameworkCore;
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
    public class PisoRepository : BaseRepository<Piso> , IPisoRepository
    {
        private readonly SGHRContext _context;
        private readonly ILogger<PisoRepository> _logger;
        private readonly IConfiguration _configuration;

        public PisoRepository(SGHRContext context, ILogger<PisoRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public override Task<OperationResult> GetAllAsync(Expression<Func<Piso, bool>> filter)
        {
            return base.GetAllAsync(filter);
        }

        public override async Task<Piso> GetEntityByIdAsync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var isValid = await BaseValidator<Piso>.ValidateID(id);
                if (!isValid.Success)
                {
                    throw new Exception("El id es invalido");
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorGetEntityAsync"];
                _logger.LogError(ex.Message , result.Message);
            }

            return await base.GetEntityByIdAsync(id);
        }

        public override async Task<OperationResult> SaveEntityAsync(Piso entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                var isValid = await BaseValidator<Piso>.ValidateEntityAsync(entity);
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

        public override async Task<OperationResult> UpdateEntityAsync(Piso entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                var isValid = await BaseValidator<Piso>.ValidateEntityAsync(entity);
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
