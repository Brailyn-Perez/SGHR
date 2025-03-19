using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SGHR.Application.DTos.habitacion.Piso
{
    public class RemovePisoDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        [NotNull]
        public int IdPiso { get; set; }
    }
}
