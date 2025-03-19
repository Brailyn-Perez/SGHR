using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SGHR.Application.DTos.habitacion.EstadoHabitacion
{
    public class RemoveEstadoHabitacionDTO
    {
        [Required]
        [NotNull]
        [Range(1, int.MaxValue)]
        public int IdEstadoHabitacion { get; set; }
    }
}
