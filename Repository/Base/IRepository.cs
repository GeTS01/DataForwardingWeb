using System.Linq.Expressions;

namespace DataForwardingWeb.Repository.Base
{
    public interface IRepository<T>
    {
        IQueryable<T> Get(Expression<Func<T, bool>> selector);
        IQueryable<T> GetAll();
        IQueryable<T> ExecuteSelectSql(string sql);
        void Create(T item);
        Task CreateAsync(T item);
        Task AddRange(IEnumerable<T> newEntities);
        void Delete(T entity);
        void Remove(T entity);
        Task RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        Task UpdateRange(IEnumerable<T> entities);
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}