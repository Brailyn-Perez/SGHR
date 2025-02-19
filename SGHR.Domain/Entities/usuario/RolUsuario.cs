
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SGHR.Domain.Entities.usuario
{
    [Table("RolUsuario", Schema = "usuario")]
    public class RolUsuario
    {
        [Key]
        public int IdRolUsuario { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        public bool? Estado { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
