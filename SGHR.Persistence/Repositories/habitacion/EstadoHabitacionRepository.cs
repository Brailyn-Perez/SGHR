
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
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter), "El filtro no puede ser nulo.");
            }

            return base.GetAllAsync(filter);
        }

        public override async Task<OperationResult> SaveEntityAsync(EstadoHabitacion entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "La entidad no puede ser nula.");
            }

            if (string.IsNullOrWhiteSpace(entity.Descripcion))
            {
                throw new ArgumentException("La descripción no puede estar vacía.", nameof(entity.Descripcion));
            }

            return await base.SaveEntityAsync(entity);
        }

        public override async Task<EstadoHabitacion> GetEntityByIdAsync(int id)
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

        public override async Task<OperationResult> UpdateEntityAsync(EstadoHabitacion entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "La entidad no puede ser nula.");
            }

            if (entity.IdEstadoHabitacion <= 0)
            {
                throw new ArgumentException("El ID debe ser un número positivo.", nameof(entity.IdEstadoHabitacion));
            }

            if (string.IsNullOrWhiteSpace(entity.Descripcion))
            {
                throw new ArgumentException("La descripción no puede estar vacía.", nameof(entity.Descripcion));
            }

            return await base.UpdateEntityAsync(entity);
        }
    }
}
