namespace SGHR.Web.Models.habitacion.Tarifa
{
    public class DetailsTarifaViewModel
    {
        public int IdTarifa { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal PrecioPorNoche { get; set; }
        public decimal Descuento { get; set; }
        public string Descripcion { get; set; }
        public bool? Estado { get; set; } = true;
    }
}
