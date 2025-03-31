
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

        public async Task<OperationResult> GeAll()
        {
            OperationResult result = new();
            try
            {
                var usuarios = await _usuarioRepository.GetAllAsync();
                result.Data = usuarios.Select(u => new UsuarioDToBase()
                {
                    NombreCompleto = u.NombreCompleto,
                    Correo = u.Correo,
                    IdRolUsuario = u.IdRolUsuario,
                    Clave = u.Clave,
                    Estado = u.Estado,
                    Borrado = u.Borrado,
                });
                result.Message = "entity octenida corectamenete";
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorOcteniendoLaEntidad:GetAllService"];
                this._logger.LogError(result.Message, ex);
            }
            return result;
        }

        public async Task<OperationResult> GeById(int id)
        {
            OperationResult result = new();
            try
            {
                var Usuario = await _usuarioRepository.GetEntityByIdAsync(id);
                result.Data = Usuario;
                result.Message = "Entidad Octenida Correctamente";
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorOcteniendoElUsuario:GetById"];
                this._logger.LogError(result.Message, ex);
            }
            return result;
        }

        public async Task<OperationResult> Save(SaveUsuarioDTo dto)
        {
            OperationResult result = new();
            try
            {
                result = await _usuarioRepository.SaveEntityAsync(new Domain.Entities.usuario.Usuario()
                {
                    NombreCompleto = dto.NombreCompleto,
                    Correo = dto.Correo,
                    IdRolUsuario = dto.IdRolUsuario,
                    Clave = dto.Clave
                });
                result.Message = "Usuario Guardado Correctamente";
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorGuardandoElUsuario:Save"];
                this._logger.LogError(result.Message, ex);
            }
            return result;
        }

        public async Task<OperationResult> Update(UpdateUsuarioDTo dto)
        {
            OperationResult result = new();
            try
            {
                var user = await _usuarioRepository.GetEntityByIdAsync(dto.IdUsuario);
                user.NombreCompleto = dto.NombreCompleto;
                user.Correo = dto.Correo;
                user.Clave = dto.Clave;
                user.FechaActualizacion = DateTime.Now;
                result = await _usuarioRepository.UpdateEntityAsync(user);
                result.Success = true;
                result.Message = "usuario Actualizsdo Correctamente";
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorActualizandoElUsuario:Update"];
                this._logger.LogError(result.Message, ex);
            }
            return result;
        }

        public Task<OperationResult> Remove(RemoveUsuarioDTo dto)
        {
            throw new NotImplementedException();
        }
    }
}
