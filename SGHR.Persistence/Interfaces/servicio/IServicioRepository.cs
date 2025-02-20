
using SGHR.Domain.Base;
using SGHR.Domain.Entities.servicio;
using SGHR.Domain.Repository;

namespace SGHR.Persistence.Interfaces.servicio
{
    public interface IServicioRepository : IBaseRepository<Servicios>
    {
        Task<OperationResult> AsociarServicioACategoriaAsync(int idServicio, int idCategoria);
        Task<List<Servicios>> ObtenerServiciosPorCategoriaAsync(int idCategoria);
    }
}