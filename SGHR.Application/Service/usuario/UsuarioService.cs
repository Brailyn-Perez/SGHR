
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Application.DTos.usuario.Usuario;
using SGHR.Application.Interfaces.usuario;
using SGHR.Domain.Base;
using SGHR.Persistence.Interfaces.usuario;

namespace SGHR.Application.Service.usuario
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<UsuarioService> _logger;
        private readonly IConfiguration _configuration;
        

        public UsuarioService(IUsuarioRepository usuarioRepository,
            ILogger<UsuarioService> logger,
            IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
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

        public Task<OperationResult> Remove(UpdateUsuarioDTo dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveUsuarioDTo dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(RemoveUsuarioDTo dto)
        {
            throw new NotImplementedException();
        }
    }
}
