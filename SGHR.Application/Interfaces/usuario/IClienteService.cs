

using SGHR.Application.Base;
using SGHR.Application.DTos.usuario.Cliente;

namespace SGHR.Application.Interfaces.usuario
{
    public interface IClienteService : IBaseService<SaveClienteDTo, RemoveClienteDTo, UpdateClienteDTo>
    {
        
    }
}
