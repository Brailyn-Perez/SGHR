

using SGHR.Application.Base;
using SGHR.Application.DTos.usuario.RolUsuario;

namespace SGHR.Application.Interfaces.usuario
{
    public interface IRolUsuarioService : IBaseService<SaveRolUsuarioDTo, RemoveRolUsuarioDTo, UpdateRolUsuarioDTo>
    {
    }
}
