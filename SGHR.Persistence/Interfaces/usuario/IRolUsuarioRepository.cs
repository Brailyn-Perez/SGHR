
using SGHR.Domain.Base;
using SGHR.Domain.Entities.usuario;
using SGHR.Domain.Repository;

namespace SGHR.Persistence.Interfaces.usuario
{
    public interface IRolUsuarioRepository : IBaseRepository<RolUsuario>
    {
        public Task<OperationResult> GetRolUsuarioByUsuario(Usuario usuario);
    }
}
