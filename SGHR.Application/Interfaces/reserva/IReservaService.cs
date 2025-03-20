using SGHR.Application.Base;
using SGHR.Application.DTos.reserva.Reserva;
using SGHR.Domain.Repository;

namespace SGHR.Application.Interfaces.reserva
{
    public interface IReservaService : IBaseService<SaveReservaDTO, UpdateReservaDTO, RemoveReservaDTO>
    {
    }
}
