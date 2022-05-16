
using ArtTalesFull.Data;
using ArtTalesFull.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtTalesFull.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext dbContext;
        public BaseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> AddAsync(T newEntity)
        {
            var addedEntity = await dbContext.Set<T>().AddAsync(newEntity);
            return addedEntity.Entity;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await dbContext.Set<T>().ToListAsync();
            return result;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var result = await dbContext.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id);

            return result;
        }

        public async Task RemoveAsync(int id)
        {

            var toRemove = await dbContext.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id);

            dbContext.Remove(await GetByIdAsync(id));
        }

        public abstract Task<T> UpdateAsync(T newInfo);

    }
}

