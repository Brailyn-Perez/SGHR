using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Base;
using SGHR.Persistence.Context;
using SGHR.Persistence.Interfaces.habitacion;

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

        public async Task<OperationResult> DeletePiso(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var existePiso = _context.Pisos.Any(x => x.IdPiso == id);
                if (!existePiso)
                {
                    result.Message = "No existe el piso a borrar";
                    result.Success = false;
                    return result;
                }

                var Piso = await _context.Pisos.FindAsync(id);

                var TieneHabitacion =  _context.Habitaciones.Any(x => x.IdPiso == Piso.IdPiso);
                if (!TieneHabitacion)
                {
                    Piso.Borrado = true;
                    _context.Pisos.Update(Piso);
                    result.Message = "Piso Eliminado";
                    result.Success = true;
                }
                else
                {
                    var Habitacion = await _context.Habitaciones.FirstAsync(x => x.IdPiso == Piso.IdPiso);
                    var tieneReserva =  await _context.Reservas.FirstAsync(x => x.IdHabitacion == Habitacion.IdHabitacion);
                    if (tieneReserva.Estado == true)
                    {
                        result.Message = "Este piso tiene reservas activas a su nombre, no se puede borrar";
                        result.Success = false;
                        return result;
                    }
                    else
                    {
                        Piso.Borrado = true;
                        _context.Pisos.Update(Piso);
                        result.Message = "Piso Eliminado";
                        result.Success = true;
                    }
                }

            }
            catch(Exception ex)
            {
                result.Message = _configuration["ErrorPisoRepository:DeletePiso"];
                result.Success = false;
                _logger.LogError(result.Message , ex.ToString());
            }

            return result;
        }
    }
}
