

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SGHR.Application.DTos.DToBase;

namespace SGHR.Application.DTos.usuario.Usuario
{
    public class UsuarioDToBase : DToBases
    {
        [Required]
        [StringLength(50)]
        public string NombreCompleto { get; set; }

        [Required]
        [StringLength(100)]
        public string Correo { get; set; }

        [ForeignKey("RolUsuario")]
        public int IdRolUsuario { get; set; }

        [Required]
        [StringLength(50)]
        public string Clave { get; set; }

        public bool? Estado { get; set; } = true;

        public bool Borrado { get; set; }
    }
}
