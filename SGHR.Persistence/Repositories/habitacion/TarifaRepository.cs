
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Base;
using SGHR.Persistence.Context;
using SGHR.Persistence.Interfaces.habitacion;

namespace SGHR.Persistence.Repositories.habitacion
{
    public class TarifaRepository : BaseRepository<Tarifa>,ITarifaRepository
    {
        private readonly SGHRContext _context;
        private readonly ILogger<TarifaRepository> _logger;
        private readonly IConfiguration _configuration;

        public TarifaRepository(SGHRContext context, ILogger<TarifaRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

    }
}
