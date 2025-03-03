
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SGHR.Domain.Entities.servicio;
using SGHR.Domain.Base;
using System.Diagnostics.CodeAnalysis;

namespace SGHR.Domain.Entities.habitacion
{
    [Table("Categoria", Schema = "habitacion")]
    public class Categoria : AuditoryEntity
    {
        [Key]
        public int IdCategoria { get; set; }
        [Required]
        [StringLength(50)]
        [MaxLength(50)]
        [MinLength(10)]
        [NotNull]
        public string Descripcion { get; set; }
        public bool? Estado { get; set; } = true;
        [Required]
        [NotNull]
        [Range(1 , int.MaxValue)]
        public int IdServicio { get; set; }

    }
}
