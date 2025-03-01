

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Application.DTos.usuario.RolUsuario;
using SGHR.Application.Interfaces.usuario;
using SGHR.Domain.Base;
using SGHR.Persistence.Interfaces.usuario;

namespace SGHR.Application.Service.usuario
{
    public class RolUsuarioService : IRolUsuarioService
    {
        private readonly IRolUsuarioRepository _rolUsuarioRepository;
        private readonly ILogger<UsuarioService> _logger;
        private readonly IConfiguration _configuration;


        public RolUsuarioService(IRolUsuarioRepository rolUsuarioRepository,
            ILogger<UsuarioService> logger,
            IConfiguration configuration)
        {
            _rolUsuarioRepository = rolUsuarioRepository;
            _logger = logger;
            _configuration = configuration;
        }
        public Task<OperationResult> GeAll()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GeById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Remove(UpdateRolUsuarioDTo dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveRolUsuarioDTo dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(RemoveRolUsuarioDTo dto)
        {
            throw new NotImplementedException();
        }
    }
}
