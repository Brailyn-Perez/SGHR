
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SGHR.Domain.Entities.habitacion
{
    [Table("EstadoHabitacion", Schema = "habitacion")]
    public class EstadoHabitacion
    {
        [Key]
        public int IdEstadoHabitacion { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        public bool? Estado { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

    }
}
