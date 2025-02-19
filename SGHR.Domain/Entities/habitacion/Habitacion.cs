
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SGHR.Domain.Entities.habitacion
{
    [Table("Habitacion", Schema = "habitacion")]
    public class Habitacion
    {
        [Key]
        public int IdHabitacion { get; set; }

        [Required]
        [StringLength(50)]
        public string Numero { get; set; }

        [Required]
        [StringLength(100)]
        public string Detalle { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Precio { get; set; }

        [ForeignKey("EstadoHabitacion")]
        public int IdEstadoHabitacion { get; set; }

        [ForeignKey("Piso")]
        public int IdPiso { get; set; }

        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }

        public bool? Estado { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public EstadoHabitacion EstadoHabitacion { get; set; }
        public Piso Piso { get; set; }

        public Categoria Categoria
        {
            get; set;
        }
    }
}
