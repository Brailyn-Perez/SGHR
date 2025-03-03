using SGHR.Application.Base;
using SGHR.Application.DTos.servicio.Servicio;

namespace SGHR.Application.Interfaces.sevicio
{
    public interface IServiciosService : IBaseService<SaveServicioDTO,UpdateServicioDTO, RemoveServicioDTO>
    {
    }
}
