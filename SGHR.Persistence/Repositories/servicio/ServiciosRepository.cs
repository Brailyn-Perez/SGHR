using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.servicio;
using SGHR.Persistence.Base;
using SGHR.Persistence.Context;
using SGHR.Persistence.Interfaces.servicio;

namespace SGHR.Persistence.Repositories.servicio
{
    public class ServiciosRepository : BaseRepository<Servicios>, IServicioRepository
    {
        private readonly SGHRContext _context;
        private readonly ILogger<ServiciosRepository> _logger;
        private readonly IConfiguration _configuration;

        public ServiciosRepository(SGHRContext context, ILogger<ServiciosRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> AsociarServicioACategoriaAsync(int idServicio, int idCategoria)
        {
            var result = new OperationResult();

            var categoria = await _context.Categorias.FindAsync(idCategoria);
            if (categoria == null)
            {
                result.Success = false;
                result.Message = "Categoría no encontrada.";
                return result;
            }

            var servicio = await GetEntityByIdAsync(idServicio);
            if (servicio == null)
            {
                result.Success = false;
                result.Message = "Servicio no encontrado.";
                return result;
            }

            if (categoria.Estado == true)
            {
                result.Success = false;
                result.Message = "No se puede asociar un servicio a una categoría inactiva.";
                return result;
            }

            categoria.IdServicio = idServicio;

            try
            {
                _context.Categorias.Update(categoria);
                await _context.SaveChangesAsync();
                result.Success = true;
                result.Message = "Servicio asociado a la categoría con éxito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al asociar el servicio a la categoría.";
                _logger.LogError(ex, "Error al asociar el servicio a la categoría.");
            }

            return result;
        }

        public async Task<List<Servicios>> ObtenerServiciosPorCategoriaAsync(int idCategoria)
        {
            var idServicio = await _context.Categorias
                .Where(c => c.IdCategoria == idCategoria)
                .Select(c => c.IdServicio)
                .FirstOrDefaultAsync();

            if (idServicio == 0) return new List<Servicios>();

            return await _context.Servicios
                .Where(s => s.IdServicio == idServicio)
                .ToListAsync();
        }

    }
}