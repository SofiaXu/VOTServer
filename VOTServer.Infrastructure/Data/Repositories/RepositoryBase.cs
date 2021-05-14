using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VOTServer.Core.Interface;
using VOTServer.Models.Interface;

namespace VOTServer.Infrastructure.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly VOTDbContext context;
        public RepositoryBase(VOTDbContext context)
        {
            this.context = context;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await context.Set<TEntity>().AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }

        public virtual async Task<int> CountAsync()
        {
            return await context.Set<TEntity>().Where(x => x.IsDelete == null).CountAsync();
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await context.Set<TEntity>().FirstOrDefaultAsync(expression);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(int pageSize, int page)
        {
            return await context.Set<TEntity>().Where(x => x.IsDelete == null).Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync();
        }

        public virtual async Task<TEntity> GetEntityByIdAsync(long id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().RemoveRange(entities);
            await context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> expression, int pageSize, int page)
        {
            return await context.Set<TEntity>().Where(expression).Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
