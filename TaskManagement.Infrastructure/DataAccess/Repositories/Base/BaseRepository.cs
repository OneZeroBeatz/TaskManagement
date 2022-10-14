
namespace TaskManagement.Infrastructure.DataAccess.Repositories.Base
{
    public abstract class BaseRepository<T> 
        where T : class
    {
        protected readonly TaskManagementDbContext DbContext;

        protected BaseRepository(TaskManagementDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<T?> FindAsync(int id, CancellationToken token)
        {
            return await DbContext.FindAsync<T>(new object[] { id }, token);
        }

        public async Task<T> InsertAsync(T entity, CancellationToken token)
        {
            DbContext.Add(entity);
            await DbContext.SaveChangesAsync(token);
            return entity;
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            DbContext.Update(entity);
            await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await DbContext.FindAsync<T>(id);
            DbContext.Remove<T>(entity!);
            await DbContext.SaveChangesAsync();
        }
    }
}
