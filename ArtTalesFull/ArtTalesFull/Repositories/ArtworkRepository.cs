using ArtTalesFull.Data;
using ArtTalesFull.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtTalesFull.Repositories
{
    public class ArtworkRepository : BaseRepository<Artwork>
    {
        public ArtworkRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Artwork>> GetAllArtworksAsync()
        {
            return await dbContext.Set<Artwork>().ToListAsync();
        }

        public async Task<List<Artwork>> GetAllArtworksForUserAsync(string id)
        {
            return await dbContext.Set<Artwork>()
                .Where(x => x.UserId == id)
                .ToListAsync();
        }

        public async Task<Artwork> GetArtworkByIdAsync(int id)
        {
            return await dbContext.Set<Artwork>()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public override Task<Artwork> UpdateAsync(Artwork newInfo)
        {
            throw new NotImplementedException();
        }

        public async Task<Artwork> GetMaxLikesArtworkAsync(string id)
        {
            return await dbContext.Set<Artwork>().Where(x => x.UserId == id).OrderByDescending(x => x.Likes).FirstOrDefaultAsync();
        }
    }
}
