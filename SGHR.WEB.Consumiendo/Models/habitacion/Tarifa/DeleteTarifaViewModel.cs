using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SGHR.WEB.Consumiendo.Models.habitacion.Tarifa
{
    public class DeleteTarifaViewModel
    {
        [Required]
        [NotNull]
        [Range(1, int.MaxValue)]
        public int IdTarifa { get; set; }
    }
}
