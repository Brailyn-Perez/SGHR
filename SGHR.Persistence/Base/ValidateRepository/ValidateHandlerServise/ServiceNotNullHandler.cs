using SGHR.Domain.Base;
using SGHR.Domain.Entities.servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGHR.Persistence.Base.ValidateRepository.ValidateHandlerServise
{
    public class ServiceNotNullHandler : IValidatorHandler<Servicios>
    {
        private IValidatorHandler<Servicios>? _next;

        public IValidatorHandler<Servicios>? SetNext(IValidatorHandler<Servicios> next)
        {
            _next = next;
            return _next;
        }

        public OperationResult Handler(Servicios servicio)
        {
            OperationResult result = new();
            if (servicio == null)
            {
                result.Success = false;
                result.Message = "Error No Se Permite EntidadesNulas";
                return result;
            }
            return _next?.Handler(servicio) ?? result;
        }
    }
}
