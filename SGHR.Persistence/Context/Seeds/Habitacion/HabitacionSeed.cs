
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SGHR.Persistence.Context.Seeds.Habitacion
{
    public class HabitacionSeed : IEntityTypeConfiguration<Domain.Entities.habitacion.Habitacion>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.habitacion.Habitacion> builder)
        {
           builder.HasQueryFilter(x => !x.Borrado);
        }
    }
}
