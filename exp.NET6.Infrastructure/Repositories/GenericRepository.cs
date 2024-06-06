using exp.NET6.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        protected readonly FlowerPowerDbContext context;
        public GenericRepository(FlowerPowerDbContext context)
        {
            this.context = context;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeletePhysical(int id)
        {
            try
            {
                var entity = await context.Set<TEntity>().FindAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException("This object does not exist, please enter a valid id");
                
                context.Set<TEntity>().Remove(entity);
                await context.SaveChangesAsync();

                return entity;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TEntity?> Get(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<bool> Exist(Expression<Func<TEntity, bool>> expression)
        {
            return await context.Set<TEntity>().AnyAsync(expression);
        }

        public IQueryable<TEntity> GetAllQuerable()
        {
            return context.Set<TEntity>().AsNoTracking().AsQueryable();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<TEntity>> RemoveRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().RemoveRange(entities);
            await context.SaveChangesAsync();
            return entities;
        }
    }
}
