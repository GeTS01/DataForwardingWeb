using DataForwardingWeb.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Storage;
using System.Linq.Expressions;

namespace DataForwardingWeb.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : PersistentObject
    {
        private readonly AppDbContext context;

        public Repository(AppDbContext context)
        {
            this.context = context;
        }

        public virtual IQueryable<T> ExecuteSelectSql(string sql)
        {
            return context.Set<T>().FromSqlRaw($"{sql}");
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> selector)
        {

            return context.Set<T>().Where(selector).AsQueryable();
        }

        public virtual async Task CreateAsync(T newEntity)
        {
            newEntity.CreatedAt = DateTime.UtcNow;
            newEntity.UpdatedAt = DateTime.UtcNow;
            await context.Set<T>().AddAsync(newEntity);
        }

        public virtual async Task AddRange(IEnumerable<T> newEntities)
        {
            await context.Set<T>().AddRangeAsync(newEntities);
        }

        public virtual async Task RemoveRange(IEnumerable<T> entities)
        {
            await Task.Run(() => context.Set<T>().RemoveRange(entities));
        }

        public virtual void Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            context.Set<T>().Update(entity);
        }

        public virtual async Task UpdateRange(IEnumerable<T> entities)
        {
            await Task.Run(() => context.Set<T>().UpdateRange(entities));
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public virtual IQueryable<T> GetAll()
        {
            return context.Set<T>().Where(x => !x.IsDeleted);
        }

        public virtual void Create(T item)
        {
            item.CreatedAt = DateTime.UtcNow;
            item.UpdatedAt = DateTime.UtcNow;
            context.Set<T>().Add(item);
        }

        public virtual void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
            context.Update(entity);
        }

        public virtual void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public virtual int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}