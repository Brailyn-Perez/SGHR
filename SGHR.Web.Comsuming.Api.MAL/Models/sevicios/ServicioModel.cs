using System.ComponentModel.DataAnnotations;

namespace SGHR.Web.Comsuming.Api.MAL.Models.sevicios
{
    public class ServicioModel
    {
        public int IdServicio { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }
    }
}
