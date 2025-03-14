using MedicalAppointment.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.reserva;
using SGHR.Persistence.Base;
using SGHR.Persistence.Context;
using SGHR.Persistence.Interfaces.reserva;
using System.Linq.Expressions;



namespace SGHR.Persistence.Repositories.reserva
{
    public class ReservaRepository : BaseRepository<Reserva>, IReservaRepository
    {
        private readonly SGHRContext _context;
        private readonly ILogger<ReservaRepository> _logger;
        private readonly IConfiguration _configuration;

        public ReservaRepository(SGHRContext context, ILogger<ReservaRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> RealizarReservaAsync(Reserva reserva)
        {
            var result = new OperationResult();

            if (reserva.FechaEntrada < DateTime.Now || reserva.FechaSalida < DateTime.Now)
            {
                result.Success = false;
                result.Message = "Las fechas seleccionadas no pueden ser anteriores a la fecha actual.";
                return result;
            }

            var habitacionDisponible = await _context.Habitaciones
                .AnyAsync(h => h.IdHabitacion == reserva.IdHabitacion && h.Estado == true);

            if (!habitacionDisponible)
            {
                result.Success = false;
                result.Message = "La habitación seleccionada no está disponible.";
                return result;
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.IdCategoria == reserva.Habitacion.IdCategoria);
            try
            {
                await SaveEntityAsync(reserva);
                result.Success = true;
                result.Message = "Reserva realizada con éxito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al realizar la reserva.";
                _logger.LogError(ex, "Error al realizar la reserva.");
            }

            return result;
        }

        public async Task<OperationResult> CancelarReservaAsync(int idReserva)
        {
            var result = new OperationResult();

            var reserva = await GetEntityByIdAsync(idReserva);

            if (reserva.FechaEntrada < DateTime.Now.AddDays(1))
            {
                result.Success = false;
                result.Message = "No se puede cancelar la reserva dentro de las 24 horas previas al check-in.";
                return result;
            }

            reserva.Estado = false;

            try
            {
                await BaseValidator<Reserva>.ValidateEntityAsync(reserva);
                await UpdateEntityAsync(reserva);
                result.Success = true;
                result.Message = "Reserva cancelada con éxito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al cancelar la reserva.";
                _logger.LogError(ex, "Error al cancelar la reserva.");
            }

            return result;
        }

        public override async Task<Reserva> GetEntityByIdAsync(int id)
        {
            try
            {
                await BaseValidator<Reserva>.ValidateID(id);
            }
            catch(Exception ex){
                this._logger.LogError(ex.Message, ex.ToString());
            }
            var data = await BaseValidator<Reserva>.ValidateEntityAsync(_context.Reservas.FindAsync(id));
            return data.Data;
        }

        public override async Task<List<Reserva>> GetAllAsync()
        {
            return await _context.Reservas.Where(r => r.Borrado == false).ToListAsync();
        }

        public override async Task<OperationResult> SaveEntityAsync(Reserva entity)
        {
            OperationResult result = new();
            Expression<Func<Reserva, bool>> filter = re => re.IdHabitacion == entity.IdHabitacion && re.FechaSalida >= entity.FechaEntrada;
            if (ExistsAsync(filter).Result)
            {
                result.Success = false;
                result.Message = "no puede reservar esta habitacion ya esta reservada elija otra fecha o elija otra habitacion";
                return result;
            }
            if(entity == null)
            {
                result.Success = false;
                result.Message = "La entidad no puede ser nula";
                return result;
            }
            try
            {
                result.Data = await SaveEntityAsync(new Reserva()
                {
                    IdCliente = entity.IdCliente,
                    IdHabitacion = entity.IdHabitacion,
                    FechaEntrada = entity.FechaEntrada,
                    FechaSalida = entity.FechaSalida,
                    PrecioInicial = entity.PrecioInicial,
                    Adelanto = entity.Adelanto,
                    Observacion = entity.Observacion,
                    NumeroHuespedes = entity.NumeroHuespedes
                });
            }catch(Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorAlRegistrarLaReserva"];
            }
            return result;
        }
    }
}