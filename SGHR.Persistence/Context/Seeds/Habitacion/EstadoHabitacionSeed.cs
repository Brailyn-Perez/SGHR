
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGHR.Domain.Entities.habitacion;

namespace SGHR.Persistence.Context.Seeds.Habitacion
{
    public class EstadoHabitacionSeed : IEntityTypeConfiguration<EstadoHabitacion>
    {
        public void Configure(EntityTypeBuilder<EstadoHabitacion> builder)
        {
            builder.HasQueryFilter(x => !x.Borrado);
        }
    }
}
