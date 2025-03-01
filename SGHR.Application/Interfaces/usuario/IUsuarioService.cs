

using SGHR.Application.Base;
using SGHR.Application.DTos.usuario.Usuario;


namespace SGHR.Application.Interfaces.usuario
{
    public interface IUsuarioService : IBaseService<SaveUsuarioDTo, RemoveUsuarioDTo, UpdateUsuarioDTo>
    {
    }
}
