using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SGHR.Web.Models.habitacion.Piso
{
    public class RemovePisoViewModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        [NotNull]
        public int IdPiso { get; set; }
    }
}
