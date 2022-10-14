namespace TaskManagement.Infrastructure.DataAccess.Repositories.Base
{
    public interface IRepository<T> 
        where T : class
    {
        Task<T?> FindAsync(int id, CancellationToken token);
        Task<T> InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
