using MedicalAppointment.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.servicio;
using SGHR.Persistence.Base;
using SGHR.Persistence.Context;
using SGHR.Persistence.Interfaces.servicio;
using SGHR.Persistence.Base.ValidateRepository;
using SGHR.Persistence.Base.ValidateRepository.ValidateHandlerServise;
using System.Net.Http.Headers;
using System.Linq.Expressions;
using SGHR.Persistence.Base.ValidateRepository.ValidateLamda;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            try
            {
                await BaseValidator<Servicios>.ValidateEntityAsync(servicio);
                await BaseValidator<Servicios>.ValidateEntityAsync(idServicio);
                _context.Categorias.Update(categoria);
                await _context.SaveChangesAsync();
                result.Success = true;
                result.Message = "Servicio asociado a la categoría con éxito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorAlAsociarElServicioAlaCategoria"];
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

        public override async Task<OperationResult> UpdateEntityAsync(Servicios servicios)
        {
            OperationResult result = new();
            var servisenotnullservice = new ServiceNotNullHandler();
            var servisenotnullproperties = new ServiseNotNullProperties();

            //result = servisenotnullservice.SetNext(servisenotnullproperties).Handler(servicios);
            if(servicios == null)
            {
                result.Success = false;
                result.Message = "Error No Se Permite EntidadesNulas";
                return result;
            }

            if (!result.Success)
            {
                return result;   
            }
            try
            {
                var entity = await GetEntityByIdAsync(servicios.IdServicio);
                entity.Nombre = servicios.Nombre;
                entity.Descripcion = servicios.Descripcion;
                entity.UsuarioActualizacion = servicios.UsuarioActualizacion;
                entity.FechaActualizacion = servicios.FechaActualizacion;

                _context.Servicios.Update(entity);
                await _context.SaveChangesAsync();
                result.Data = entity;
                result.Message = "Servicio actualizadoCorrectamente";
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorAlActualizarLaEntidad:UpDateEntityAsync"];
                this._logger.LogError(result.Message, ex);
            }
            return result;
        }

        public override async Task<List<Servicios>> GetAllAsync()
        {
            return await _context.Servicios.Where(s => s.Borrado == false).ToListAsync();
        }

        public override async Task<Servicios> GetEntityByIdAsync(int id)
        {
            var servicenotnullhandler = new ServiceNotNullHandler();
            if(id <= 0)
            {
                throw new ArgumentException("El id No puede ser menor o ingual a 0", nameof(id));
            }
            var entity = await _context.Servicios.FindAsync(id);
            if (!servicenotnullhandler.Handler(entity).Success)
            {
                throw new KeyNotFoundException($"entidad no encontrada id:{id} no encontrado");
            }
            return entity;
        }

        public override async Task<OperationResult> SaveEntityAsync(Servicios servicios)
        {
            OperationResult result = new();
            var servisenotnullproperties = new ServiseNotNullProperties();
            var servicenotnullhandler = new ServiceNotNullHandler();

            result = servicenotnullhandler
                        .SetNext(servisenotnullproperties)
                        .Handler(servicios);

            if(!result.Success)
            {
                return result;
            }

            try
            {
                result.Data = _context.Servicios.Add(new Servicios()
                {
                    Nombre = servicios.Nombre,
                    Descripcion = servicios.Descripcion,
                    UsuarioCreacion = servicios.UsuarioCreacion,
                    FechaCreacion = DateTime.Now,
                    Borrado = false
                }).Entity;
                await _context.SaveChangesAsync();
                result.Message = "Datos Guardados Correctamente";

            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorGuardandoLaEntidad:SaveEntityAsync"];
                this._logger.LogError(result.Message, ex);
            }
            return result;
        }

        public override async Task<bool> ExistsAsync(Expression<Func<Servicios, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter), "El filtro no puede ser nulo.");
            }
            var filtroConBorrado = filter.And(servicio => servicio.Borrado == false);
            return await _context.Servicios.AnyAsync(filtroConBorrado);
        }

        public override async Task<OperationResult> GetAllAsync(Expression<Func<Servicios, bool>> filter)
        {
            OperationResult result = new();

            if (filter == null)
            {
                filter = servicio => false;
            }
            var filtroConBorrado = filter.And(servicio => servicio.Borrado == false);
            try
            {
                result.Data = await _context.Servicios.Where(filtroConBorrado).ToListAsync();
                result.Success = true;
                result.Message = "Datos Obtenidos Correctamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error Obteniendo los datos: {ex.Message}";
            }
            return result;
        }

    }
}