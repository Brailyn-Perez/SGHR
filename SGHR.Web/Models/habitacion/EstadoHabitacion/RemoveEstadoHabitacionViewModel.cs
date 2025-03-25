using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SGHR.Web.Models.habitacion.EstadoHabitacion
{
    public class RemoveEstadoHabitacionViewModel
    {
        [Required]
        [NotNull]
        [Range(1, int.MaxValue)]
        public int IdEstadoHabitacion { get; set; }
    }
}
