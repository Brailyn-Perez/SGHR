

using SGHR.Domain.Base;

namespace SGHR.Application.Base
{
    public interface IBaseService<DToSave, DToUpdate, DToRemove>
    {
        Task<OperationResult> GeAll();
        Task<OperationResult> GeById(int id);
        Task<OperationResult> Remove(DToRemove dto);
        Task<OperationResult> Update(DToUpdate dto);
        Task<OperationResult> Save(DToSave dto);
    }
}
