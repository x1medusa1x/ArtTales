using ArtTalesFull.Data;
using ArtTalesFull.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtTalesFull.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await dbContext.Set<ApplicationUser>().ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetAllUsersWithNameAsync(string name)
        {
            return await dbContext.Set<ApplicationUser>().Where(x => x.AppUsername == name).ToListAsync();
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await dbContext.Set<ApplicationUser>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(string id)
        {

            var toRemove = await dbContext.Set<ApplicationUser>().FirstOrDefaultAsync(entity => entity.Id == id);

            dbContext.Remove(await GetByIdAsync(id));

        }


        //public override Task<ApplicationUser> UpdateAsync(ApplicationUser newInfo)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
