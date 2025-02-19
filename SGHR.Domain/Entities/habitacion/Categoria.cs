
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SGHR.Domain.Entities.servicio;

namespace SGHR.Domain.Entities.habitacion
{
    [Table("Categoria", Schema = "habitacion")]
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        public bool? Estado { get; set; } = true;

        [ForeignKey("Servicios")]
        public int IdServicio { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public Servicios Servicios { get; set; }

    }
}
