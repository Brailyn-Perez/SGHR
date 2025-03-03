using SGHR.Application.DTos.DToBase;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SGHR.Application.DTos.habitacion.Tarifa
{
    public class RemoveTarifaDTO : DToBases
    {
        [Required]
        [NotNull]
        [Range(1, int.MaxValue)]
        public int IdTarifa { get; set; }
    }
}
