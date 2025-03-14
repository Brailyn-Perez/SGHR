
using MedicalAppointment.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.usuario;
using SGHR.Model.Model.usuario;
using SGHR.Persistence.Base;
using SGHR.Persistence.Context;
using SGHR.Persistence.Interfaces.usuario;
using SGHR.Persistence.Repositories.habitacion;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace SGHR.Persistence.Repositories.usuario
{
    public class RolUsuarioRepository : BaseRepository<RolUsuario>, IRolUsuarioRepository
    {
        private readonly SGHRContext _context;
        private readonly ILogger<RolUsuarioRepository> _logger;
        private readonly IConfiguration _configuration;

        public RolUsuarioRepository(SGHRContext context, ILogger<RolUsuarioRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> GetRolUsuarioByUsuario(Usuario usuario)
        {
            OperationResult result = new OperationResult();

            try
            {
                var Query = await (from u in _context.Usuarios
                                   join r in _context.RolUsuarios on u.IdRolUsuario equals r.IdRolUsuario
                                   where r.IdRolUsuario == usuario.IdUsuario
                                   select new RolUsuarioUsuarioModel()
                                   {
                                       IdUsuario = u.IdUsuario,
                                       IdRolUsuario = r.IdRolUsuario,
                                       NombreCompleto = u.NombreCompleto,
                                       Clave = u.Clave,
                                       Descripcion = r.Descripcion,
                                       Estado = r.Estado,
                                       FechaCreacion = r.FechaCreacion,

                                   }).ToListAsync();

                if (Query == null || !Query.Any()) 
                {
                    result.Message = "Consulta no encontrada";
                    result.Success = false;
                }

                result.Data = Query;
                result.Success = true;
            }
            catch (Exception ex) 
            {
                result.Message = _configuration["ErrorRolUsuarioRepository:GetRolUsuarioByUsuario"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public override async Task<OperationResult> SaveEntityAsync(RolUsuario entity)
        {
            OperationResult result = new OperationResult();
            try 
            {
                return await base.SaveEntityAsync(entity);
            }
            catch (Exception ex) 
            {
                result.Message = _configuration["ErrorRolUsuarioRepository:SaveEntityAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            result.Success = true;
            return result;
        }
        public override async Task<RolUsuario> GetEntityByIdAsync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var validator = await BaseValidator<RolUsuario>.ValidateID(id);
                if (!validator.Success) 
                {
                    throw new Exception("El Id es invalido");
                }
            }
            catch (Exception ex) 
            {
                result.Message = _configuration["ErrorRolUsuarioRepository:GetEntityByIdAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return await base.GetEntityByIdAsync(id);
        }
        public override async Task<OperationResult> UpdateEntityAsync(RolUsuario entity)
        {
            OperationResult result = new OperationResult();

            try 
            {
                var validator = await base.ExistsAsync(x => x.IdRolUsuario == entity.IdRolUsuario);
                if (!validator)
                {
                    result.Message = "El Rol no existe";
                    result.Success = false;
                }
                else
                {
                    return await base.UpdateEntityAsync(entity);
                }
            }
            catch (Exception ex) 
            {
                result.Message = _configuration["ErrorRolUsuarioRepository:UpdateEntityAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public override async Task<List<RolUsuario>> GetAllAsync()
        {
            return await _context.RolUsuarios.Where(cd => cd.Borrado == false).ToListAsync();
        }

    }
}
