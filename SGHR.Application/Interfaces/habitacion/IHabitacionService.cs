
using SGHR.Application.Base;
using SGHR.Application.DTos.habitacion.Habitacion;

namespace SGHR.Application.Interfaces.habitacion
{
    public interface IHabitacionService : IBaseService<SaveHabitacionDTO,UpdateHabitacionDTO,RemoveHabitacionDTO>
    {
    }
}
