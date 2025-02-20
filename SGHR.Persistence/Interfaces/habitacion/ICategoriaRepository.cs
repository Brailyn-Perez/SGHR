
using SGHR.Domain.Base;
using SGHR.Domain.Entities.habitacion;
using SGHR.Domain.Entities.servicio;
using SGHR.Domain.Repository;

namespace SGHR.Persistence.Interfaces.habitacion
{
    public interface ICategoriaRepository : IBaseRepository<Categoria>
    {
        public Task<OperationResult> GetCategoriaByServicios(Servicios servicios);
        public Task<OperationResult> GetAllCategoriasDisponibles();
    }
}
