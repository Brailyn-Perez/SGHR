using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SGHR.Web.Comsuming.Api.MAL.Models.usuario
{
    public class ClienteModel
    {
        [Key]
        public int IdCliente { get; set; }

        public string TipoDocumento { get; set; }

        public string Documento { get; set; }

        public string NombreCompleto { get; set; }

        public string Correo { get; set; }

        public bool? Estado { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
