using SGHR.Application.DTos.DToBase;
using System.ComponentModel.DataAnnotations;

namespace SGHR.Application.DTos.usuario.Cliente
{
    public class ClienteDToBase: DToBases
    {
        [Required]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(15)]
        [RegularExpression("^[a-zA-Z]+$")]
        public string TipoDocumento { get; set; }

        [Required]
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

        public bool Borrado { get; set; }

        public bool? Estado { get; set; } = true;
    }
}
