using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SGHR.Application.DTos.habitacion.Categoria
{
    public class RemoveCategoriaDTO
    {
        [NotNull]
        [Required]
        [Range(1, int.MaxValue)]
        public int IdCategoria { get; set; }
    }
}
