using SGHR.Application.DTos.DToBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGHR.Application.DTos.usuario.RolUsuario
{
    public class RolUsuarioDToBase: DToBases
    {
        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        public bool? Estado { get; set; } = true;

        public bool Borrado { get; set; }
    }
}
