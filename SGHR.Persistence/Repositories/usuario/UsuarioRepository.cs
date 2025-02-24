
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.usuario;
using SGHR.Persistence.Base;
using SGHR.Persistence.Context;
using SGHR.Persistence.Interfaces.usuario;
using SGHR.Persistence.Repositories.habitacion;
using System.Runtime.CompilerServices;

namespace SGHR.Persistence.Repositories.usuario
{
    public class UsuarioRepository : BaseRepository<Usuario >, IUsuarioRepository
    {
        private readonly SGHRContext _context;
        private readonly ILogger<UsuarioRepository> _logger;
        private readonly IConfiguration _configuration;

        public UsuarioRepository(SGHRContext context, ILogger<UsuarioRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> ValidarUsuario(Usuario usuario)
        {
           OperationResult result = new OperationResult();
            try
            {
                var validacion = await _context.Usuarios.AnyAsync(c => c.Correo == usuario.Correo && c.Clave == usuario.Clave);

                if (!validacion) 
                {
                    result.Message = "Usuario no registrado";
                    result.Success = false;
                }

                result.Message = "Usuario Encontrado";
                result.Success = true;
               

            }
            catch (Exception ex) 
            {
                result.Message = _configuration["ErrorUsuarioRepository:ValidarUsuario"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());

            }
            return result;
        }
    }
}
