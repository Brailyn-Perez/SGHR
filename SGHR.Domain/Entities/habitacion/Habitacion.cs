
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SGHR.Domain.Base;
using System.Diagnostics.CodeAnalysis;

namespace SGHR.Domain.Entities.habitacion
{
    [Table("Habitacion", Schema = "habitacion")]
    public class Habitacion : AuditoryEntity
    {
        [Key]
        public int IdHabitacion { get; set; }
        [Required]
        [StringLength(50)]
        [MaxLength(50)]
        [NotNull]
        public string Numero { get; set; }
        [Required]
        [StringLength(100)]
        [MaxLength(100)]
        [MinLength(10)]
        [NotNull]
        public string Detalle { get; set; }
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Precio { get; set; }

        public int IdEstadoHabitacion { get; set; }
        public int IdPiso { get; set; }
        public int IdCategoria { get; set; }
        public bool? Estado { get; set; } = true;

    }
}
