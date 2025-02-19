
using SGHR.Domain.Base;
using SGHR.Domain.Entities.habitacion;
using SGHR.Domain.Repository;

namespace SGHR.Persistence.Interfaces.habitacion
{
    public interface ITarifaRepository : IBaseRepository<Tarifa>
    {
        public Task<OperationResult> DefinirPrecioBase(decimal precioBase);
    }
}
