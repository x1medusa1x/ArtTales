using ArtTalesFull.Abstractions;
using ArtTalesFull.Data;
using ArtTalesFull.Repositories;

namespace ArtTalesFull
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepository UserRepository { get; set; }

        public ArtworkRepository ArtworkRepository { get; set; }

        public ImageRepository ImageRepository { get; set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            UserRepository = new UserRepository(dbContext);
            ArtworkRepository = new ArtworkRepository(dbContext);
            ImageRepository = new ImageRepository(dbContext);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
