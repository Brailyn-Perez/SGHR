
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SGHR.Domain.Base;

namespace SGHR.Domain.Entities.habitacion
{
    [Table("Piso", Schema = "habitacion")]
    public class Piso : AuditoryEntity
    {
        [Key]
        public int IdPiso { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        public bool? Estado { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
