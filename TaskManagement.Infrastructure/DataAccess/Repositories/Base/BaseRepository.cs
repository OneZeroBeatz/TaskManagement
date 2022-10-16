
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

        public async Task<T?> FindAsync(int id, CancellationToken token = default)
        {
            return await DbContext.FindAsync<T>(new object[] { id }, token);
        }

        public async Task<T> InsertAsync(T entity, CancellationToken token = default)
        {
            DbContext.Add(entity);
            await DbContext.SaveChangesAsync(token);
            return entity;
        }

        public async Task UpdateAsync(T entity, CancellationToken token = default)
        {
            DbContext.Update(entity);
            await DbContext.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(int id, CancellationToken token = default)
        {
            var entity = await DbContext.FindAsync<T>(id);

            if (entity == null)
                return;

            DbContext.Remove(entity!);
            await DbContext.SaveChangesAsync(token);
        }
    }
}
