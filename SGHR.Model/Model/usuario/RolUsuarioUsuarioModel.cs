

namespace SGHR.Model.Model.usuario
{
    public class RolUsuarioUsuarioModel
    {
        public int IdUsuario { get; set; }
        public int IdRolUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public bool? Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
