
using SGHR.Domain.Base;
using SGHR.Domain.Repository;
using System.Linq.Expressions;

namespace SGHR.Persistence.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetEntityByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> SaveEntityAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> UpdateEntityAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
