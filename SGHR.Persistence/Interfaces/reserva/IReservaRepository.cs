using SGHR.Domain.Base;
using SGHR.Domain.Entities.reserva;
using SGHR.Domain.Repository;

namespace SGHR.Persistence.Interfaces.reserva
{
    public interface IReservaRepository : IBaseRepository<Reserva>
    {
        Task<OperationResult> RealizarReservaAsync(Reserva reserva);
        Task<OperationResult> CancelarReservaAsync(int idReserva);
    }
}