

namespace SGHR.Application.DTos.reserva.Reserva
{
    public class ReservaDTO : ReservaBaseDTO
    {
       public int IdReserva { get; set; }
       public DateTime FechaSalidaConfirmacion { get; set; }
       public decimal PrecioRestante { get; set; }
       public decimal TotalPagado { get; set; }
       public decimal CostoPenalidad { get; set; }
       public bool? Estado { get; set; }
    }
}
