using MedicalAppointment.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.habitacion;
using SGHR.Domain.Entities.reserva;
using SGHR.Domain.Entities.servicio;
using SGHR.Domain.Entities.usuario;
using SGHR.Persistence.Base;
using SGHR.Persistence.Base.ValidateRepository.ValidateLamda;
using SGHR.Persistence.Base.ValidateRepository.ValidateReservaHandler;
using SGHR.Persistence.Context;
using SGHR.Persistence.Interfaces.reserva;
using System.Linq;
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
            OperationResult result = new();
            var reservanotnullentity = new ReservaNotNullEntity();
            if(id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }
            var reserva = await _context.Reservas.FindAsync(id);
            result = reservanotnullentity.Handler(reserva);
            if (!result.Success)
            {
                throw new KeyNotFoundException($"Entidad no encontrada id:{id} no encontrado");
            }
            var entity = (Reserva)result.Data;
            return entity;
        }

        public override async Task<List<Reserva>> GetAllAsync()
        {
            return await _context.Reservas
                .Where(r => !r.Borrado)
                .ToListAsync();
        }

        public override async Task<OperationResult> SaveEntityAsync(Reserva reserva)
        {
            OperationResult result = new();
            // Crear instancias de validadores
            var validnullenty = new ReservaNotNullEntity();
            var validproperty = new ReservaNotNullProperties();

            // Configurar la cadena correctamente
            validnullenty.SetNext(validproperty);

            // Iniciar la validación con el primer handler
            result = validnullenty.Handler(reserva);

            // Si la validación fue exitosa, recuperar la entidad validada
            if (result.Success && result.Data != null)
            {
                result.Message = "La entidad no puede ser nula.";
                return result;
            }
            var entity = (Reserva)result.Data;
            try
            {
                var nuevaReserva = new Reserva()
                {
                    IdCliente = entity.IdCliente,
                    IdHabitacion = entity.IdHabitacion,
                    FechaEntrada = entity.FechaEntrada,
                    FechaSalida = entity.FechaSalida,
                    FechaSalidaConfirmacion = entity.FechaSalida,
                    PrecioInicial = entity.PrecioInicial,
                    Adelanto = entity.Adelanto,
                    PrecioRestante = entity.PrecioInicial - entity.Adelanto,
                    TotalPagado = entity.Adelanto,
                    CostoPenalidad = 0.00m,
                    Observacion = entity.Observacion,
                    NumeroHuespedes = entity.NumeroHuespedes,
                    Estado = entity.Estado,
                    UsuarioCreacion = entity.UsuarioCreacion,
                    FechaCreacion = entity.FechaCreacion,
                    Borrado = false
                };
                var entryResult = await _context.Reservas.AddAsync(nuevaReserva);
                await _context.SaveChangesAsync();
                result.Data = entryResult.Entity;
                result.Message = "Entidad guardada exitosamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorAlRegistrarLaReserva"];
            }
            return result;
        }

        public override async Task<OperationResult> UpdateEntityAsync(Reserva reservas)
        {
            OperationResult result = new();
            var validnullenty = new ReservaNotNullEntity();
            var validproperty = new ReservaNotNullProperties();
            validnullenty.SetNext(validproperty);
            result = validnullenty.Handler(reservas);
            if (result.Success && result.Data != null)
            {
                return result;
            }
            var entity = (Reserva)result.Data;
            if (entity.UsuarioActualizacion <= 0)
            {
                result.Success = false;
                result.Message = "Error Actualizando la reserva El usuario Actualizacion deve se pasado";
                return result;
            }
            try
            {
                var reserva = await GetEntityByIdAsync(entity.IdReserva);
                reserva.FechaEntrada = entity.FechaEntrada;
                reserva.FechaSalida = entity.FechaSalida;
                reserva.FechaSalidaConfirmacion = entity.FechaSalida;
                reserva.PrecioInicial = entity.PrecioInicial;
                reserva.Adelanto = entity.Adelanto;
                reserva.PrecioRestante = entity.PrecioInicial - entity.Adelanto;
                reserva.TotalPagado = entity.TotalPagado;
                reserva.CostoPenalidad = 0;
                reserva.Observacion = entity.Observacion;
                reserva.NumeroHuespedes = entity.NumeroHuespedes;
                reserva.UsuarioActualizacion = entity.UsuarioActualizacion;
                reserva.Borrado = false;
                reserva.FechaActualizacion = DateTime.Now;
                _context.Reservas.Update(reserva);
                await _context.SaveChangesAsync();
                result.Data = reserva;
                result.Message = "Reserva actualizadoCorrectamente";
            }catch(Exception ex)
            {
                result.Message = _configuration["ErrorActuelizandoLaReservva:UpdateEntityAsync"];
                this._logger.LogError(result.Message, ex);
            }
            return result;
        }

        public override async Task<bool> ExistsAsync(Expression<Func<Reserva, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter), "El filtro no puede ser nulo.");
            }
            var filtroConBorrado = filter.And(reserva => reserva.Borrado == false);
            var result = await _context.Reservas.AnyAsync(filtroConBorrado);
            if (!result)
            {
                throw new KeyNotFoundException("No se encontraron registros con el filtro proporcionado.");
            }
            return true;
        }

        public override async Task<OperationResult> GetAllAsync(Expression<Func<Reserva, bool>> filter)
        {
            var result = new OperationResult();
            try
            {
                var query = _context.Reservas.AsQueryable();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                query = query.Where(r => !r.Borrado);

                result.Data = await query.ToListAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

    }
}