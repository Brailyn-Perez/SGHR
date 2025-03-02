using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SGHR.Application.DTos.habitacion.Tarifa
{
    public class UpdateTarifaDTO
    {
        [Required]
        [NotNull]
        [Range(1,int.MaxValue)]
        public int IdTarifa { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int IdHabitacion { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime FechaFin { get; set; }
        [Required]
        public decimal PrecioPorNoche { get; set; }
        [Required]
        public decimal Descuento { get; set; }
        [Required]
        [StringLength(255)]
        [MaxLength(255)]
        [MinLength(10)]
        [NotNull]
        public string Descripcion { get; set; }
        public bool? Estado { get; set; } = true;
    }
}
