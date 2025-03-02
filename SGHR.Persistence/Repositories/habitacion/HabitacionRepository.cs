
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

         
    }
}
