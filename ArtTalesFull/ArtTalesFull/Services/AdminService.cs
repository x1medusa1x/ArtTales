using ArtTalesFull.Abstractions;
using ArtTalesFull.Entities;
using ArtTalesFull.Models;
using Microsoft.AspNetCore.Identity;

namespace ArtTalesFull.Services
{
    public class AdminService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public async Task<EditUserModel> GetEditUserAsync(string id)
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(id);
            return new EditUserModel
            {
                UserId = user.Id,
                Username = user.AppUsername,
                Email = user.Email,
                Disabled = user.Disabled
            };
        }
        public async Task<EditUserModel> GetDeleteUserAsync(string id)
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(id);
            return new EditUserModel
            {
                UserId = user.Id,
                Username = user.AppUsername,
                Email = user.Email,
                Disabled = user.Disabled
            };
        }

        public async Task EditUser(EditUserModel editUserModel)
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(editUserModel.UserId);

            user.Email = editUserModel.Email;
            user.Disabled = editUserModel.Disabled;
            user.AppUsername = editUserModel.Username;

            await unitOfWork.SaveAsync();
        }

        public async Task DeleteUser(EditUserModel editUserModel)
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(editUserModel.UserId);
            var artworks = await unitOfWork.ArtworkRepository.GetAllArtworksForUserAsync(editUserModel.UserId);
            foreach (var artwork in artworks)
            {
                var images = await unitOfWork.ImageRepository.GetAllImagesForArtworkAsync(artwork.Id);
                foreach (var image in images)
                {
                    await unitOfWork.ImageRepository.RemoveAsync(image.Id);

                    string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                    fullPath = Path.Combine(fullPath, image.Image);
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }
                    await unitOfWork.SaveAsync();
                }
                await unitOfWork.ArtworkRepository.RemoveAsync(artwork.Id);
                await unitOfWork.SaveAsync();
            }

            await unitOfWork.UserRepository.RemoveAsync(user.Id);

            await unitOfWork.SaveAsync();
        }

    }
}
