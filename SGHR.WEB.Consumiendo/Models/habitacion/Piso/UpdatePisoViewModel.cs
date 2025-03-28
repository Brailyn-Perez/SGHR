using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SGHR.WEB.Consumiendo.Models.habitacion.Piso
{
    public class UpdatePisoViewModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        [NotNull]
        public int IdPiso { get; set; }
        [Required]
        [StringLength(50)]
        [MaxLength(50)]
        [MinLength(10)]
        [NotNull]
        public string Descripcion { get; set; }
        public bool? Estado { get; set; } = true;
    }
}
