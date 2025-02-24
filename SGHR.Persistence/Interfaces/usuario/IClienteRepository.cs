
using SGHR.Domain.Base;
using SGHR.Domain.Entities.usuario;
using SGHR.Domain.Repository;

namespace SGHR.Persistence.Interfaces.usuario
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        public Task<OperationResult> GetCienteByReservas(Cliente cliente);

    }
}
