using SGHR.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGHR.Persistence.Base.ValidateRepository
{
    public interface IValidatorHandler<T>
    {
        IValidatorHandler<T>? SetNext(IValidatorHandler<T> next);
        OperationResult Handler(T entity);
    }
}
