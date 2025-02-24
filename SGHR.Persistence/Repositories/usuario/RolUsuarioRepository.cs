
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.usuario;
using SGHR.Model.Model.usuario;
using SGHR.Persistence.Base;
using SGHR.Persistence.Context;
using SGHR.Persistence.Interfaces.usuario;
using SGHR.Persistence.Repositories.habitacion;

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
                result.Message = this._configuration["ErrorRolUsuarioRepository:GetRolUsuarioByUsuario"];
                result.Success = false;
                this._logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }
    }


}
