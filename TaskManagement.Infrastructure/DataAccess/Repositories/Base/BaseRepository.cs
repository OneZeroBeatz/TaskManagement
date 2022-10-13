
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

        public async Task<T?> FindAsync(int id)
        {
            return await DbContext.FindAsync<T>(id);
        }

        public async Task<T> InsertAsync(T entity)
        {
            DbContext.Add(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            DbContext.Update(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await DbContext.FindAsync<T>(id);
            DbContext.Remove<T>(entity!);
            await DbContext.SaveChangesAsync();
        }
    }
}
