using Microsoft.EntityFrameworkCore.Query;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.reserva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGHR.Persistence.Base.ValidateRepository.ValidateReservaHandler
{
    public class ReservaNotNullEntity : IValidatorHandler<Reserva>
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

            if (reserva == null)
            {
                result.Success = false;
                result.Message = "La Reserva no puede ser nula";
                return result;
            }

            result.Data = reserva;

            var nextResult = _next?.Handler(reserva) ?? result;

            if (nextResult.Success && nextResult.Data == null)
            {
                nextResult.Data = reserva;
            }

            return nextResult;
        }
    }
}
