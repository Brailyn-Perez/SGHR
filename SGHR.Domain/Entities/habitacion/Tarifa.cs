
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SGHR.Domain.Base;
using System.Diagnostics.CodeAnalysis;

namespace SGHR.Domain.Entities.habitacion
{
    [Table("Tarifas", Schema = "habitacion")]
    public class Tarifa : AuditoryEntity
    {
        [Key]
        public int IdTarifa { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int IdHabitacion { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime FechaFin { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal PrecioPorNoche { get; set; }
        [Required]
        [Column(TypeName = "numeric(5, 2)")]
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
