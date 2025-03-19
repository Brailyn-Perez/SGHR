
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGHR.Domain.Entities.habitacion;

namespace SGHR.Persistence.Context.Seeds.Habitacion
{
    public class CategoriaSeed : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasQueryFilter(x => !x.Borrado);
        }
    }
}
