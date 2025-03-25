namespace SGHR.Application.DTos.habitacion.EstadoHabitacion
{
    public class EstadoHabitacionDTO
    {
        public int IdEstadoHabitacion { get; set; }
        public string Descripcion { get; set; }
        public bool? Estado { get; set; } = true;
    }
}
