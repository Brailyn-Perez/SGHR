

using System.ComponentModel.DataAnnotations;

namespace SGHR.Application.DTos.usuario.Cliente
{
    public class RemoveClienteDTo: ClienteDToBase
    {
        [Key]
        public int IdCliente { get; set; }


    }
}
