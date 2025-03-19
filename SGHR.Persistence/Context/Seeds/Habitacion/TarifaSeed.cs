
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGHR.Domain.Entities.habitacion;

namespace SGHR.Persistence.Context.Seeds.Habitacion
{
    public class TarifaSeed : IEntityTypeConfiguration<Domain.Entities.habitacion.Tarifa>
    {
        public void Configure(EntityTypeBuilder<Tarifa> builder)
        {
            builder.HasQueryFilter(x => !x.Borrado);
        }
    }
}
