
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SGHR.Domain.Base;

namespace SGHR.Domain.Entities.habitacion
{
    [Table("Tarifas", Schema = "habitacion")]
    public class Tarifa : AuditoryEntity
    {
        [Key]
        public int IdTarifa { get; set; }

        [ForeignKey("Habitacion")]
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
        public string Descripcion { get; set; }

        public bool? Estado { get; set; } = true;

        public Habitacion Habitacion { get; set; }
    }
}
