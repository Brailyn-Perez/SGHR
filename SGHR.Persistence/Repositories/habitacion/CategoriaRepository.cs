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
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        private readonly ILogger<CategoriaRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly SGHRContext _context;

        public CategoriaRepository(SGHRContext context, ILogger<CategoriaRepository> logger, IConfiguration configuration)
            : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public override async Task<OperationResult> SaveEntityAsync(Categoria entity)
        {
            OperationResult result = new();
            try
            {
                    return await base.SaveEntityAsync(entity);
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorCategoriaRepository:SaveEntityAsync"] ?? "Error al guardar la categoría";
                result.Success = false;
                _logger.LogError(result.Message, ex);
            }

            result.Success = true;
            return result;
        }

        public override async Task<OperationResult> UpdateEntityAsync(Categoria entity)
        {
            OperationResult result = new();
            try
            {
                bool existeCategoria = await base.ExistsAsync(x => x.IdCategoria == entity.IdCategoria);
                if (!existeCategoria)
                {
                    result.Message = "La categoría a editar no existe";
                    result.Success = false;
                }
                else
                {
                    return await base.UpdateEntityAsync(entity);
                }
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorCategoriaRepository:UpdateEntityAsync"] ?? "Error al actualizar la categoría";
                result.Success = false;
                _logger.LogError(result.Message, ex);
            }
            return result;
        }

        public override Task<OperationResult> GetAllAsync(Expression<Func<Categoria, bool>> filter)
        {
            return base.GetAllAsync(filter);
        }

        public override Task<Categoria> GetEntityByIdAsync(int id)
        {
            return base.GetEntityByIdAsync(id);
        }

        public async Task<OperationResult> GetHabitacionByCategoriaId(int Id)
        {
            OperationResult result = new();
            try
            {
               var isValid = await BaseValidator<Categoria>.ValidateID(Id);
                if (!isValid.Success)
                    return isValid;

                var exist = await _context.Habitaciones.AnyAsync(x => x.IdCategoria == Id);

                if (exist)
                {
                    result.Success = true;
                    result.Message = "Hay una habitacion asignada";
                }
                else
                {
                    result.Success = false;
                    result.Message = "No hay habitacion asignada";
                }
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorCategoriaRepository:SaveEntityAsync"] ?? "Error al guardar la categoría";
                result.Success = false;
                _logger.LogError(result.Message, ex);
            }
            return result;
        }
    }
}
