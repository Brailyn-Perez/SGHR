

using System.ComponentModel.DataAnnotations;

namespace SGHR.Application.DTos.usuario.Usuario
{
    public class UpdateUsuarioDTo: UsuarioDToBase
    {
        [Key]
        public int IdUsuario { get; set; }
    }
}
