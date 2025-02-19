
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Base;
using SGHR.Persistence.Context;
using SGHR.Persistence.Interfaces.habitacion;

namespace SGHR.Persistence.Repositories.habitacion
{
    public class HabitacionRepository : BaseRepository<Habitacion> , IHabitacionRepository
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

        public Task<OperationResult> GetHabitacionByCategoria(Categoria categoria)
        {
            OperationResult result = new  OperationResult();
            try
            {
                
            }catch(Exception ex)
            {
                result.Message = _configuration["ErrorHabitacionRepository:GetHabitacionByCategoria"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetHabitacionByEstado(EstadoHabitacion estadoHabitacion)
        {
            OperationResult result = new OperationResult();
            try
            {

            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorHabitacionRepository:GetHabitacionByEstado"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetHabitacionByPiso(Piso piso)
        {
            OperationResult result = new OperationResult();
            try
            {

            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorHabitacionRepository:GetHabitacionByPiso"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            throw new NotImplementedException();
        }

         
    }
}
