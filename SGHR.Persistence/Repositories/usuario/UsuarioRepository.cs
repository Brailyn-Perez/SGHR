
using MedicalAppointment.Persistence.Base;
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
        public override async Task<OperationResult> SaveEntityAsync(Usuario entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                return await base.SaveEntityAsync(entity);
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorUsuarioRepository:SaveEntityAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            result.Success = true;
            return result;
        }
        public override async Task<Usuario> GetEntityByIdAsync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var validator = await BaseValidator<Usuario>.ValidateID(id);
                if (!validator.Success)
                {
                    throw new Exception("El Id es invalido");
                }
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorUsuarioRepository:GetEntityByIdAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return await base.GetEntityByIdAsync(id);
        }
        public override async Task<OperationResult> UpdateEntityAsync(Usuario entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                var validator = await base.ExistsAsync(x => x.IdUsuario == entity.IdUsuario);
                if (!validator)
                {
                    result.Message = "El usuario no existe";
                    result.Success = false;
                }
                else
                {
                    return await base.UpdateEntityAsync(entity);
                }
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorUsuarioRepository:UpdateEntityAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public override async Task<List<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.Where(cd => cd.Borrado == false).ToListAsync();
        }
    }
}
