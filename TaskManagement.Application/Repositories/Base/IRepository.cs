namespace TaskManagement.Infrastructure.DataAccess.Repositories.Base
{
    public interface IRepository<T> 
        where T : class
    {
        Task<T?> FindAsync(int id, CancellationToken token);
        Task<T> InsertAsync(T entity, CancellationToken token);
        Task UpdateAsync(T entity, CancellationToken token);
        Task DeleteAsync(int id);
    }
}
