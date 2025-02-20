

namespace SGHR.Model.Model.usuario
{
    public class ClienteReservasModel
    {
        public int IdCliente { get; set; }
        public int IdReserva { get; set; }
        public int IdHabitacion { get; set; }
        public string? TipoDocumento { get; set; }
        public string? Documento { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public DateTime FechaEntrada { get; set; }
        public decimal PrecioInicial { get; set; }
        public decimal Adelanto { get; set; }
        public decimal PrecioRestante { get; set; }
        public decimal CostoPenalidad { get; set; }
        public string Observacion { get; set; }
        public decimal TotalPagado { get; set; }
        public bool? Estado { get; set; }
    }
}
