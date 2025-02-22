
namespace SGHR.Domain.Base
{
    public class AuditoryEntity
    {
        public AuditoryEntity()
        {
            FechaCreacion = DateTime.Now;
            Borrado = false;
        }

        public int UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? UsuarioEliminacion { get; set; }
        public DateTime? FechaEliminado { get; set; }
        public bool Borrado { get; set; }
        public int? UsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
