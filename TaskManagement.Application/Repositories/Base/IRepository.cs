namespace TaskManagement.Infrastructure.DataAccess.Repositories.Base
{
    public interface IRepository<T> 
        where T : class
    {
        Task DeleteAsync(int id);
        Task<T?> FindAsync(int id);
        Task<T> InsertAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
