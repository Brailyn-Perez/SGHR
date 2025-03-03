
using SGHR.Application.Base;
using SGHR.Application.DTos.habitacion.Categoria;

namespace SGHR.Application.Interfaces.habitacion
{
    public interface ICategoriaService : IBaseService<SaveCategoriaDTO,UpdateCategoriaDTO,RemoveCategoriaDTO>
    {
    }
}
