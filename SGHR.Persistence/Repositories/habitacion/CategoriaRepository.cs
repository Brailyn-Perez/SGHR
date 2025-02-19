
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

        public Task<OperationResult> GetAllCategoriasDisponibles()
        {
            OperationResult result = new OperationResult();
            try
            {

            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorCategoriaRepository:GetAllCategoriasDisponibles"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetCategoriaByServicios(Servicios servicios)
        {
            OperationResult result = new OperationResult();
            try
            {

            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorCategoriaRepository:GetCategoriaByServicios"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            throw new NotImplementedException();
        }
    }
}
