


using System.ComponentModel.DataAnnotations;

namespace SGHR.Application.DTos.usuario.RolUsuario
{
    public class UpdateRolUsuarioDTo: RolUsuarioDToBase
    {
        [Key]
        public int IdRolUsuario { get; set; }
    }
}
