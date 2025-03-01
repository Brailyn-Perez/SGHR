using SGHR.Application.DTos.DToBase;
using System.ComponentModel.DataAnnotations;

namespace SGHR.Application.DTos.usuario.Cliente
{
    public class ClienteDToBase: DToBases
    {
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

        public bool Borrado { get; set; }

        public bool? Estado { get; set; } = true;
    }
}
