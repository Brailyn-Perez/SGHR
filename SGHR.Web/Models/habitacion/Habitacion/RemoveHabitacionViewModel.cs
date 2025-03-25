using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SGHR.Web.Models.habitacion.Habitacion
{
    public class RemoveHabitacionViewModel
    {
        [Required]
        [NotNull]
        [Range(1, int.MaxValue)]
        public int IdHabitacion { get; set; }
    }
}
