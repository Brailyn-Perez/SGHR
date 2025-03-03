
namespace SGHR.Application.DTos.habitacion.Habitacion
{
    public class HabitacionDTO
    {
        public int IdHabitacion { get; set; }
        public string Numero { get; set; }
        public string Detalle { get; set; }
        public decimal Precio { get; set; }
        public bool? Estado { get; set; } = true;
    }
}
