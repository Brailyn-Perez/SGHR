namespace SGHR.Application.DTos.habitacion.Categoria
{
    public class CategoriaDTO
    {
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public bool? Estado { get; set; } = true;
    }
}
