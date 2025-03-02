using SGHR.Application.Base;
using SGHR.Application.DTos.habitacion.EstadoHabitacion;

namespace SGHR.Application.Interfaces.habitacion
{
    public interface IEstadoHabitacionService : IBaseService<SaveEstadoHabitacionDTO, UpdateEstadoHabitacionDTO,RemoveEstadoHabitacionDTO>
    {
    }
}
