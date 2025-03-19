using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SGHR.Application.DTos.habitacion.Habitacion
{
    public class RemoveHabitacionDTO
    {
        [Required]
        [NotNull]
        [Range(1,int.MaxValue)]
        public int IdHabitacion { get; set; }
    }
}
