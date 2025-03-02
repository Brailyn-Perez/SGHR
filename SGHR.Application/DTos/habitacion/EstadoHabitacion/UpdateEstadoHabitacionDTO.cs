
using SGHR.Application.DTos.DToBase;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SGHR.Application.DTos.habitacion.EstadoHabitacion
{
    public class UpdateEstadoHabitacionDTO : DToBases
    {
        [Required]
        [NotNull]
        [Range(1, int.MaxValue)]
        public int IdEstadoHabitacion { get; set; }
        [Required]
        [StringLength(50)]
        [MaxLength(50)]
        [MinLength(10)]
        [NotNull]
        public string Descripcion { get; set; }
        public bool? Estado { get; set; } = true;
    }
}
