﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SGHR.Application.DTos.DToBase;

namespace SGHR.Application.DTos.habitacion.Habitacion
{
    public class SaveHabitacionDTO : DToBases
    {
        [Required]
        [StringLength(50)]
        [MaxLength(50)]
        [NotNull]
        public string Numero { get; set; }
        [Required]
        [StringLength(100)]
        [MaxLength(100)]
        [MinLength(10)]
        [NotNull]
        public string Detalle { get; set; }
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Precio { get; set; }
        public bool? Estado { get; set; } = true;
    }
}
