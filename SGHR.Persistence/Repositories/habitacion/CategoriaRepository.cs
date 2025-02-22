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
        private readonly SGHRContext _context;
        private readonly ILogger<CategoriaRepository> _logger;
        private readonly IConfiguration _configuration;

        public CategoriaRepository(SGHRContext context, ILogger<CategoriaRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public override async Task<OperationResult> SaveEntityAsync(Categoria entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (await ExistsAsync(x => x.IdCategoria == entity.IdCategoria))
                {
                    result.Message = "La categoría ya existe.";
                    result.Success = false;
                }
                else
                {
                    result = await base.SaveEntityAsync(entity);
                }
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorCategoriaRepository:SaveEntityAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public override async Task<List<Categoria>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public override Task<OperationResult> GetAllAsync(Expression<Func<Categoria, bool>> filter)
        {
            return base.GetAllAsync(filter);
        }

        public override async Task<Categoria> GetEntityByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser un número positivo.", nameof(id));
            }

            var categoria = await base.GetEntityByIdAsync(id);

            if (categoria == null)
            {
                throw new KeyNotFoundException("La categoría con el ID especificado no existe.");
            }

            return categoria;
        }

        public async Task<OperationResult> DeleteCategoria(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var categoria = await _context.Categorias.FindAsync(id);
                if (categoria == null)
                {
                    result.Message = "La categoría no existe.";
                    result.Success = false;
                }
                else
                {
                    categoria.Borrado = true;
                    _context.Categorias.Update(categoria);
                    await _context.SaveChangesAsync();

                    result.Message = "Categoría eliminada con éxito.";
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorCategoriaRepository:DeleteCategoria"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public override async Task<OperationResult> UpdateEntityAsync(Categoria entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                var existeCategoria = await ExistsAsync(x => x.IdCategoria == entity.IdCategoria);
                if (existeCategoria)
                {
                    result = await base.UpdateEntityAsync(entity);
                    result.Message = "categoria editada";
                    result.Success = true;
                }
                else
                {
                    result.Message = "La categoría a editar no existe.";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorCategoriaRepository:UpdateEntityAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
    }
}
