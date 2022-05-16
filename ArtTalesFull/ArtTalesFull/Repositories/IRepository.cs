using ArtTalesFull.Entities;

namespace ArtTalesFull.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> UpdateAsync(T newInfo);
        Task RemoveAsync(int id);
        Task<T> AddAsync(T newEntity);
    }
}
