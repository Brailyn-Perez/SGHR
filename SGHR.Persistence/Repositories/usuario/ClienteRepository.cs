
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.usuario;
using SGHR.Model.Model.usuario;
using SGHR.Persistence.Base;
using SGHR.Persistence.Context;
using SGHR.Persistence.Interfaces.usuario;


namespace SGHR.Persistence.Repositories.usuario
{
    public class ClienteRepository : BaseRepository<Cliente> , IClienteRepository
    {
        private readonly SGHRContext _context;
        private readonly ILogger<ClienteRepository> _logger;
        private readonly IConfiguration _configuration;

        public ClienteRepository(SGHRContext context, ILogger<ClienteRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> GetCienteByReservas(Cliente cliente)
        {
            OperationResult result = new OperationResult();
            try
            {
                var Query = await (from c in _context.Clientes
                                   join r in _context.Reservas on c.IdCliente equals r.IdCliente
                                   where r.IdCliente == cliente.IdCliente
                                   select new ClienteReservasModel()
                                   {
                                       IdCliente = c.IdCliente,
                                       IdReserva = r.IdReserva,
                                       IdHabitacion = r.IdHabitacion,
                                       TipoDocumento = c.TipoDocumento,
                                       Documento = c.Documento,
                                       NombreCompleto = c.NombreCompleto,
                                       Correo = c.Correo,
                                       FechaEntrada = r.FechaEntrada,
                                       PrecioInicial = r.PrecioInicial,
                                       Adelanto = r.Adelanto,
                                       PrecioRestante = r.PrecioRestante,
                                       CostoPenalidad = r.CostoPenalidad,
                                       Observacion = r.Observacion,
                                       TotalPagado = r.TotalPagado,
                                       Estado = r.Estado,


                                   }).ToListAsync();
                result.Data = Query;
            }
            catch (Exception ex) 
            {
                result.Message = this._configuration["ErrorClienteRepository:GetCienteByReservas"];
                result.Success = false;
                this._logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }
    }
}
