
using SGHR.Domain.Base;
using SGHR.Domain.Entities.habitacion;
using SGHR.Domain.Repository;

namespace SGHR.Persistence.Interfaces.habitacion
{
    public interface IHabitacionRepository : IBaseRepository<Habitacion>
    {
        public Task<OperationResult> GetHabitacionByEstado(EstadoHabitacion estadoHabitacion);
        public Task<OperationResult> GetHabitacionByPiso(Piso piso);
        public Task<OperationResult> GetHabitacionByCategoria(Categoria categoria);
    }
}
