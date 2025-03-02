
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SGHR.Domain.Base;
using System.Diagnostics.CodeAnalysis;

namespace SGHR.Domain.Entities.habitacion
{
    [Table("EstadoHabitacion", Schema = "habitacion")]
    public class EstadoHabitacion : AuditoryEntity
    {
        [Key]
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
