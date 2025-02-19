
using Microsoft.EntityFrameworkCore;
using SGHR.Domain.Base;
using SGHR.Domain.Repository;
using SGHR.Persistence.Context;
using System.Linq.Expressions;

namespace SGHR.Persistence.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly SGHRContext _Context;
        private DbSet<TEntity> Entity { get; set; }
        private OperationResult Result = new OperationResult();

        public BaseRepository(SGHRContext context)
        {
            _Context = context;
            Entity = _Context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetEntityByIdAsync(int id)
        {
            return await Entity.FindAsync(id);
        }

        public virtual async Task<OperationResult> UpdateEntityAsync(TEntity entity)
        {
            try
            {
                Entity.Update(entity);
                await _Context.SaveChangesAsync();
                Result.Success = true;

            }
            catch (Exception ex)
            {
                Result.Success = false;
                Result.Message = "Error Actualizando los datos";
            }

            return Result;
        }

        public virtual async Task<OperationResult> SaveEntityAsync(TEntity entity)
        {
            try
            {
                Entity.Add(entity);
                await _Context.SaveChangesAsync();
                Result.Success = true;

            }
            catch (Exception ex)
            {
                Result.Success = false;
                Result.Message = "Error ingresando  los datos";
            }

            return Result;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await Entity.ToListAsync();

        }

        public virtual async Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                await Entity.Where(filter).ToListAsync();
                Result.Success = true;

            }
            catch (Exception ex)
            {
                Result.Success = false;
                Result.Message = "Error Obteniendo los datos";
            }

            return Result;
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Entity.AnyAsync(filter);
        }
    }
}

