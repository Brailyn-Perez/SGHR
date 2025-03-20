using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.servicio;
using SGHR.Persistence.Repositories.servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGHR.Persistence.Base.ValidateRepository.ValidateHandlerServise
{
    public class ServiseIdNotnullHandler : IValidatorHandler<Servicios>
    {
        IValidatorHandler<Servicios> _next;

        public IValidatorHandler<Servicios>? SetNext(IValidatorHandler<Servicios> next)
        {
            _next = next;
            return _next;
        }

        public OperationResult Handler(Servicios entity)
        {
            OperationResult result = new();
            if(entity.IdServicio <= 0)
            {
                result.Success = false;
                result.Message = "el id deve ser Mayor que 0";
                return result;
            }
            return _next?.Handler(entity) ?? result;
        }

    }
}
