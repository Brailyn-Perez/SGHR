using SGHR.Application.Base;
using SGHR.Application.DTos.habitacion.Tarifa;

namespace SGHR.Application.Interfaces.habitacion
{
    public interface ITarifaService : IBaseService<SaveTarifaDTO,UpdateTarifaDTO,RemoveTarifaDTO>
    {
    }
}
