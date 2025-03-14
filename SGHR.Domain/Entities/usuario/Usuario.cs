
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SGHR.Domain.Base;

namespace SGHR.Domain.Entities.usuario
{
    [Table("Usuario", Schema = "usuario")]
    public class Usuario : AuditoryEntity
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z]+$")]
        public string NombreCompleto { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Correo { get; set; }

        [ForeignKey("RolUsuario")]
        public int IdRolUsuario { get; set; }

        [Required]
        [StringLength(50)]
        public string Clave { get; set; }

        public bool? Estado { get; set; } = true;

        public DateTime? FechaCreacion { get; set; } = DateTime.Now;

        public RolUsuario RolUsuario { get; set; }

    }
}
