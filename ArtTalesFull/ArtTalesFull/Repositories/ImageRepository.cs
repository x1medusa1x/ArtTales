using ArtTalesFull.Data;
using ArtTalesFull.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtTalesFull.Repositories
{
    public class ImageRepository : BaseRepository<Images>
    {
        public ImageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Images>> GetAllImagesAsync()
        {
            return await dbContext.Set<Images>()
                .ToListAsync();
        }

        public async Task<List<Images>> GetAllImagesForNameAsync(string name)
        {
            return await dbContext.Set<Images>()
                .Where(x => x.Image == name)
                .ToListAsync();
        }

        public async Task<Images> GetAllImagesForNameAndArtworkIdAsync(string name, int artworkId)
        {
            return await dbContext.Set<Images>().FirstOrDefaultAsync(x => x.Image == name && x.ArtworkId == artworkId);
        }

        public async Task<List<Images>> GetAllImagesForArtworkAsync(int id)
        {
            return await dbContext.Set<Images>()
                .Where(x => x.ArtworkId == id)
                .ToListAsync();
        }

        public async Task<Images> GetImagesByIdAsync(int id)
        {
            return await dbContext.Set<Images>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public override Task<Images> UpdateAsync(Images newInfo)
        {
            throw new NotImplementedException();
        }
    }
}
