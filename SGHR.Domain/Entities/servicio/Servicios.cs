
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SGHR.Domain.Entities.servicio
{
    [Table("Servicios", Schema = "servicio")]
    public class Servicios
    {
        [Key]
        public int IdServicio { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }
    }
}
