using System.ComponentModel.DataAnnotations;

namespace SGHR.Web.Comsuming.Api.MAL.Models.usuario
{
    public class RolUsuarioModel
    {
        public int IdRolUsuario { get; set; }

        public string Descripcion { get; set; }

        public bool? Estado { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
