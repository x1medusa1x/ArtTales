using ArtTalesFull.Abstractions;
using ArtTalesFull.Entities;
using ArtTalesFull.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using System.Security.Claims;
using System.Text.Json;

namespace ArtTalesFull.Services
{
    public class ArtworkService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;

        public ArtworkService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public async Task<UploadModel> GetUploadAsync()
        {
            return null;
        }

        public async Task<PostModel> GetPostAsync(int index, string id, string currentUser)
        {
            var artwork = await unitOfWork.ArtworkRepository.GetArtworkByIdAsync(index);
            var artworks = await unitOfWork.ArtworkRepository.GetAllArtworksForUserAsync(id);
            var artworkId = 0;

            for (var i = 0; i < artworks.Count; i++)
            {
                if (artworks[i].Id == artwork.Id)
                {
                    artworkId = i;
                    break;
                }
            }

            return new PostModel
            {
                Id = index,
                ArtworkId = artworkId,
                UserId = id,
                CurrentUser = currentUser,
                Images = await unitOfWork.ImageRepository.GetAllImagesForArtworkAsync(index),
                Description = artwork.Description,
                Name = artwork.Name,
                Likes = artwork.Likes,
                Headline = artwork.Headline,
            };
        }

        public async Task<UploadModel> EditPostAsync(int index)
        {
            var artwork = await unitOfWork.ArtworkRepository.GetArtworkByIdAsync(index);
            return new UploadModel
            {
                Id = index,
                StringImages = await unitOfWork.ImageRepository.GetAllImagesForArtworkAsync(index),
                ModalDescription = artwork.Description,
                ModalName = artwork.Name,
                PreviewImage = artwork.PreviewImage,
                ModalHeadline = artwork.Headline,
            };
        }

        public async Task AddArtwork(UploadModel uploadModel, ClaimsPrincipal claimsPrincipal)
        {
            var user = await userManager.GetUserAsync(claimsPrincipal);
            var files = uploadModel.FileImages;

            Artwork artwork = new()
            {
                Description = uploadModel.Description,
                Name = uploadModel.Name,
                PreviewImage = uploadModel.PreviewImage,
                Headline = uploadModel.Headline,
                Likes = 0,
                UserId = user.Id,
            };

            var addedArtwork = await unitOfWork.ArtworkRepository.AddAsync(artwork);

            await unitOfWork.SaveAsync();

            if (files != null)
            {
                foreach (var file in files)
                {

                    var fileName = Path.GetFileName(file.FileName);

                    fileName = String.Concat(fileName.Where(c => !Char.IsWhiteSpace(c)));

                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

                    var extension = Path.GetExtension(fileName);

                    fileNameWithoutExtension = String.Concat(fileNameWithoutExtension.Where(c => Char.IsLetterOrDigit(c)));

                    fileName = $"{fileNameWithoutExtension}{extension}";

                    var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{fileName}";

                    using (FileStream fs = System.IO.File.Create(filepath))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }

                    Images image = new()
                    {
                        ArtworkId = addedArtwork.Id,
                        Image = fileName
                    };

                    var addedImage = await unitOfWork.ImageRepository.AddAsync(image);

                    await unitOfWork.SaveAsync();

                }
            }
        }

        private bool InStringArray(List<string> arr, string elem)
        {
            foreach (var i in arr)
            {
                if (i == elem)
                    return true;
            }
            return false;
        }

        public async Task EditArtwork(UploadModel uploadModel, ClaimsPrincipal claimsPrincipal)
        {
            var user = await userManager.GetUserAsync(claimsPrincipal);
            var currentImages = JsonSerializer.Deserialize<List<string>>(uploadModel.NewStringImages);
            var deletedImages = JsonSerializer.Deserialize<List<string>>(uploadModel.DeletedImages);
            var files = uploadModel.FileImages;

            var artwork = await unitOfWork.ArtworkRepository.GetArtworkByIdAsync(uploadModel.ArtworkId);

            artwork.Description = uploadModel.Description;
            artwork.Likes = uploadModel.Likes;
            artwork.Headline = uploadModel.Headline;
            artwork.Name = uploadModel.Name;
            artwork.PreviewImage = uploadModel.PreviewImage;

            await unitOfWork.SaveAsync();

            if (files != null)
            {
                foreach (var file in files)
                {

                    var fileName = Path.GetFileName(file.FileName);

                    if (this.InStringArray(deletedImages, fileName) == true)
                    {
                        foreach (var j in (await unitOfWork.ImageRepository.GetAllImagesAsync()))
                        {
                            if (j.ArtworkId == uploadModel.ArtworkId && j.Image == fileName)
                            {
                                var imagesWithName = await unitOfWork.ImageRepository.GetAllImagesForNameAsync(j.Image);

                                if (imagesWithName.Count == 1)
                                {
                                    string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                                    fullPath = Path.Combine(fullPath, fileName);
                                    if (File.Exists(fullPath))
                                    {
                                        File.Delete(fullPath);
                                    }
                                }

                                await unitOfWork.ImageRepository.RemoveAsync(j.Id);

                                await unitOfWork.SaveAsync();
                            }
                        }
                    }
                    else if (this.InStringArray(currentImages, fileName) != true)
                    {

                        fileName = String.Concat(fileName.Where(c => !Char.IsWhiteSpace(c)));

                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

                        var extension = Path.GetExtension(fileName);

                        fileNameWithoutExtension = String.Concat(fileNameWithoutExtension.Where(c => Char.IsLetterOrDigit(c)));

                        fileName = $"{fileNameWithoutExtension}{extension}";

                        var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{fileName}";

                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }

                        Images image = new()
                        {
                            ArtworkId = uploadModel.ArtworkId,
                            Image = fileName
                        };

                        var addedImage = await unitOfWork.ImageRepository.AddAsync(image);

                        await unitOfWork.SaveAsync();
                    }
                }
            }
        }

        public async Task LikePostAsync(int artworkId, int value)
        {
            var artwork = await unitOfWork.ArtworkRepository.GetByIdAsync(artworkId);
            artwork.Likes += value;
            await unitOfWork.SaveAsync();
        }

        public async Task ChangeProfilePic(IFormFile profilePic, ClaimsPrincipal claimsPrincipal)
        {
            var user = await userManager.GetUserAsync(claimsPrincipal);
            var fileName = Path.GetFileName(profilePic.FileName);

            fileName = String.Concat(fileName.Where(c => !Char.IsWhiteSpace(c)));

            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            var extension = Path.GetExtension(fileName);

            fileNameWithoutExtension = String.Concat(fileNameWithoutExtension.Where(c => Char.IsLetterOrDigit(c)));

            fileName = $"{fileNameWithoutExtension}_profilePic{extension}";

            var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{fileName}";

            using (FileStream fs = System.IO.File.Create(filepath))
            {
                profilePic.CopyTo(fs);
                fs.Flush();
            }

            Images image = new()
            {
                ArtworkId = -1,
                Image = fileName
            };

            var updateUser = await unitOfWork.UserRepository.GetByIdAsync(user.Id);

            updateUser.ProfilePic = fileName;

            var addedImage = await unitOfWork.ImageRepository.AddAsync(image);

            await unitOfWork.SaveAsync();
        }
    }
}
