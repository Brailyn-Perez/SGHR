
using SGHR.Domain.Base;
using SGHR.Domain.Entities.habitacion;
using SGHR.Domain.Repository;

namespace SGHR.Persistence.Interfaces.habitacion
{
    public interface ICategoriaRepository : IBaseRepository<Categoria>
    {
        public Task<OperationResult> GetHabitacionByCategoriaId(int Id);
        public Task<bool> ServicioExiste(int Id);
    }
}
