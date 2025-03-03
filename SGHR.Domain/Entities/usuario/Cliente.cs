
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SGHR.Domain.Base;

namespace SGHR.Domain.Entities.usuario
{
    [Table("Cliente", Schema = "usuario")]
    public class Cliente : AuditoryEntity
    {
        [Key]
        public int IdCliente { get; set; }

        [StringLength(15)]
        [RegularExpression("^[a-zA-Z]+$")]
        public string TipoDocumento { get; set; }

        [StringLength(15)]
        [RegularExpression("^[0-9]+$")]
        public string Documento { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z]+$")]
        public string NombreCompleto { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Correo { get; set; }

        public bool? Estado { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
