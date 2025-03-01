

using System.ComponentModel.DataAnnotations;

namespace SGHR.Application.DTos.usuario.Cliente
{
    public class UpdateClienteDTo : ClienteDToBase

    {
        [Key]
        public int IdCliente { get; set; }


    }
}
