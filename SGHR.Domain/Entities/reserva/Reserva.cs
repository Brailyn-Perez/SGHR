
using SGHR.Domain.Entities.habitacion;
using SGHR.Domain.Entities.usuario;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SGHR.Domain.Entities.reserva
{
    [Table("Reserva", Schema = "reserva")]
    public class Reserva
    {
        [Key]
        public int IdReserva { get; set; }

        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        [ForeignKey("Habitacion")]
        public int IdHabitacion { get; set; }

        [Required]
        public DateTime FechaEntrada { get; set; } = DateTime.Now;

        [Required]
        public DateTime FechaSalida { get; set; }

        public DateTime FechaSalidaConfirmacion { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PrecioInicial { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Adelanto { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal PrecioRestante { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalPagado { get; set; } = 0;

        [Column(TypeName = "decimal(10, 2)")]
        public decimal CostoPenalidad { get; set; } = 0;

        [StringLength(500)]
        public string Observacion { get; set; }

        public bool? Estado { get; set; }

        public Cliente Cliente { get; set; }
        public Habitacion Habitacion
        {
            get; set;
        }
    }
}
