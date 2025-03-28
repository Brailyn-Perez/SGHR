using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SGHR.WEB.Consumiendo.Models.habitacion.Habitacion
{
    public class DeleteHabitacionViewModel
    {
        [Required]
        [NotNull]
        [Range(1, int.MaxValue)]
        public int IdHabitacion { get; set; }
    }
}
