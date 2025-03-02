
using SGHR.Application.DTos.DToBase;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SGHR.Application.DTos.habitacion.Piso
{
    public class RemovePisoDTO :DToBases
    {
        [Required]
        [Range(1, int.MaxValue)]
        [NotNull]
        public int IdPiso { get; set; }
    }
}
