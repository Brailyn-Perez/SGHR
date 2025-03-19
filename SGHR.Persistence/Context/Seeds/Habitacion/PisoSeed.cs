
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGHR.Domain.Entities.habitacion;

namespace SGHR.Persistence.Context.Seeds.Habitacion
{
    public class PisoSeed : IEntityTypeConfiguration<Domain.Entities.habitacion.Piso>
    {
        public void Configure(EntityTypeBuilder<Piso> builder)
        {
            builder.HasQueryFilter(x => !x.Borrado);
        }
    }
}
