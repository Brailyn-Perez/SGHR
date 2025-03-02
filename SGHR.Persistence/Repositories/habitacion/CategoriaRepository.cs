
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.habitacion;
using SGHR.Domain.Entities.servicio;
using SGHR.Persistence.Base;
using SGHR.Persistence.Context;
using SGHR.Persistence.Interfaces.habitacion;

namespace SGHR.Persistence.Repositories.habitacion
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        private readonly SGHRContext _context;
        private readonly ILogger<CategoriaRepository> _logger;
        private readonly IConfiguration _configuration;

        public CategoriaRepository(SGHRContext context , ILogger<CategoriaRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> DeleteCategoria(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                var categoria = await _context.Categorias.FindAsync(id);

                if (categoria == null)
                {
                    result.Message = "La Categoria no existe";
                    result.Success = false;
                    return result;
                }

                var existeHabitacion = _context.Habitaciones.Any(x => x.IdCategoria == id);

                if (existeHabitacion)
                {
                    result.Message = "No se puede borrar una categoria con una habitacion asignada";
                    result.Success = false;
                    return result;
                }
                else
                {
                    categoria.Borrado = true;
                    _context.Categorias.Update(categoria);

                    result.Message = "Categoria eliminada con exito";
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
              var ExisteCategoria = await  base.ExistsAsync(x => x.IdCategoria == entity.IdCategoria);
                if(ExisteCategoria)
                {
                    await base.UpdateEntityAsync(entity);
                }
                else
                {
                    result.Message = "La categoria a editar no existe";
                    result.Success = false;
                }

            }catch(Exception ex)
            {
                result.Message = _configuration["ErrorCategoriaRepository:UpdateEntityAsync"];
                result.Success=false;
                _logger.LogError(result.Message , ex.ToString());
            }

            return result;
        }



    }
}
