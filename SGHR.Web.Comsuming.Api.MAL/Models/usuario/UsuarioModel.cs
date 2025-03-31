namespace SGHR.Web.Comsuming.Api.MAL.Models.usuario
{
    public class UsuarioModel
    {
        public int IdUsuario { get; set; }

        public string NombreCompleto { get; set; }

        public string Correo { get; set; }

        public int IdRolUsuario { get; set; }

        public string Clave { get; set; }

        public bool? Estado { get; set; } = true;

        public DateTime? FechaCreacion { get; set; } = DateTime.Now;
    }
}
