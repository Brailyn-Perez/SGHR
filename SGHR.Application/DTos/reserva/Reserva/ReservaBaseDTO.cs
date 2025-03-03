

namespace SGHR.Application.DTos.reserva.Reserva
{
    public class ReservaBaseDTO
    {
        public int IdCliente { get; set; }
        public int IdHabitacion { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public decimal PrecioInicial { get; set; }
        public decimal Adelanto { get; set; }
        public string Observacion { get; set; }
        public int NumeroHuespedes { get; set; }
    }
}
