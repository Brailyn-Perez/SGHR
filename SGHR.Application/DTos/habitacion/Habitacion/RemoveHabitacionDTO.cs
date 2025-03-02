using SGHR.Application.DTos.DToBase;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SGHR.Application.DTos.habitacion.Habitacion
{
    public class RemoveHabitacionDTO : DToBases
    {
        [Required]
        [NotNull]
        [Range(1,int.MaxValue)]
        public int IdHabitacion { get; set; }
    }
}
