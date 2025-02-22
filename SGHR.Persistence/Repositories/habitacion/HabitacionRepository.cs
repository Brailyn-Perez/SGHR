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
        private readonly SGHRContext _context;
        private readonly ILogger<HabitacionRepository> _logger;
        private readonly IConfiguration _configuration;

        public HabitacionRepository(SGHRContext context, ILogger<HabitacionRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public override async Task<OperationResult> SaveEntityAsync(Habitacion entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "La entidad no puede ser nula.");
            }

            if (string.IsNullOrWhiteSpace(entity.Detalle))
            {
                throw new ArgumentException("La descripción no puede estar vacía.", nameof(entity.Detalle));
            }

            return await base.SaveEntityAsync(entity);
        }

        public override async Task<OperationResult> GetAllAsync(Expression<Func<Habitacion, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter), "El filtro no puede ser nulo.");
            }

            return await base.GetAllAsync(filter);
        }

        public override async Task<Habitacion> GetEntityByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser un número positivo.", nameof(id));
            }

            var entity = await base.GetEntityByIdAsync(id);

            if (entity == null)
            {
                throw new KeyNotFoundException("La entidad con el ID especificado no existe.");
            }

            return entity;
        }

        public override async Task<OperationResult> UpdateEntityAsync(Habitacion entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "La entidad no puede ser nula.");
            }

            if (entity.IdHabitacion <= 0)
            {
                throw new ArgumentException("El ID debe ser un número positivo.", nameof(entity.IdHabitacion));
            }

            if (string.IsNullOrWhiteSpace(entity.Detalle))
            {
                throw new ArgumentException("La descripción no puede estar vacía.", nameof(entity.Detalle));
            }

            return await base.UpdateEntityAsync(entity);
        }

        public async Task<OperationResult> DeleteHabitacion(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                var habitacion = await _context.Habitaciones.FindAsync(id);

                if (habitacion == null)
                {
                    result.Message = "La habitación no existe.";
                    result.Success = false;
                    return result;
                }
                else
                {
                    habitacion.Borrado = true;
                    _context.Habitaciones.Update(habitacion);
                    await _context.SaveChangesAsync();

                    result.Message = "Habitación eliminada con éxito.";
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorHabitacionRepository:DeleteHabitacion"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }
    }
}