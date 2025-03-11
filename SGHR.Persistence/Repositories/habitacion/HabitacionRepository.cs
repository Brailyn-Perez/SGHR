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
    public class HabitacionRepository : BaseRepository<Habitacion>, IHabitacionRepository
    {
        private readonly ILogger<HabitacionRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly SGHRContext _context;

        public HabitacionRepository(SGHRContext context, ILogger<HabitacionRepository> logger, IConfiguration configuration)
            : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public override async Task<OperationResult> SaveEntityAsync(Habitacion entity)
        {
            OperationResult result = new();
            try
            {
                return await base.SaveEntityAsync(entity);
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorHabitacionRepository:SaveEntityAsync"] ?? "Error al guardar la habitación";
                result.Success = false;
                _logger.LogError(result.Message, ex);
            }
            return result;
        }

        public override async Task<OperationResult> UpdateEntityAsync(Habitacion entity)
        {
            OperationResult result = new();
            try
            {
                bool existeHabitacion = await base.ExistsAsync(x => x.IdHabitacion == entity.IdHabitacion);
                if (!existeHabitacion)
                {
                    result.Message = "La habitación a editar no existe";
                    result.Success = false;
                }
                else
                {
                    return await base.UpdateEntityAsync(entity);
                }
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorHabitacionRepository:UpdateEntityAsync"] ?? "Error al actualizar la habitación";
                result.Success = false;
                _logger.LogError(result.Message, ex);
            }
            return result;
        }

        public override Task<OperationResult> GetAllAsync(Expression<Func<Habitacion, bool>> filter)
        {
            return base.GetAllAsync(filter);
        }

        public override Task<Habitacion> GetEntityByIdAsync(int id)
        {
            return base.GetEntityByIdAsync(id);
        }
    }
}
