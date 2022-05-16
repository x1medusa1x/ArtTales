using ArtTalesFull.Repositories;

namespace ArtTalesFull.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        public UserRepository UserRepository { get; set; }

        public ArtworkRepository ArtworkRepository { get; set; }

        public ImageRepository ImageRepository { get; set; }

        public Task SaveAsync();
    }
}
