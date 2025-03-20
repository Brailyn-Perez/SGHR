using SGHR.Domain.Base;
using SGHR.Domain.Entities.reserva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGHR.Persistence.Base.ValidateRepository.ValidateReservaHandler
{
    public class ReservaIdNotNull : IValidatorHandler<Reserva>
    {

        private IValidatorHandler<Reserva>? _next;

        public IValidatorHandler<Reserva>? SetNext(IValidatorHandler<Reserva> next)
        {
            _next = next;
            return _next;
        }

        public OperationResult Handler(Reserva reserva)
        {
            OperationResult result = new();

            if(reserva.IdReserva <= 0)
            {
                result.Success = false;
                result.Message = "El id no peude ser menor o igual a 0";
            }

            return _next?.Handler(reserva) ?? result;
        }
    }
}
