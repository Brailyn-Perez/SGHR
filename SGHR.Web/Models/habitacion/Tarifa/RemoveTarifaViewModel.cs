using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SGHR.Web.Models.habitacion.Tarifa
{
    public class RemoveTarifaViewModel
    {
        [Required]
        [NotNull]
        [Range(1, int.MaxValue)]
        public int IdTarifa { get; set; }
    }
}
