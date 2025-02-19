
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SGHR.Domain.Entities.usuario
{
    [Table("Cliente", Schema = "usuario")]
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [StringLength(15)]
        public string TipoDocumento { get; set; }

        [StringLength(15)]
        public string Documento { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreCompleto { get; set; }

        [Required]
        [StringLength(100)]
        public string Correo { get; set; }

        public bool? Estado { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
