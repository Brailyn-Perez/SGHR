using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SGHR.Web.Models.habitacion.Categoria
{
    public class RemoveCategoriaViewModel
    {
        [NotNull]
        [Required]
        [Range(1, int.MaxValue)]
        public int IdCategoria { get; set; }
    }
}
