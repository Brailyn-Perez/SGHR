
using System.ComponentModel.DataAnnotations;

namespace SGHR.Application.DTos.usuario.Usuario
{
    public class RemoveUsuarioDTo : UsuarioDToBase
    {
        [Key]
        public int IdUsuario { get; set; }

    }
}
