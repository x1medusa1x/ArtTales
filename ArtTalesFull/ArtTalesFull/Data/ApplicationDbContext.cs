using ArtTalesFull.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArtTalesFull.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(new IdentityRole[] {
                new IdentityRole("Admin"){Id = "78f77ce2-4753-4f0b-9826-5dab25018afa"},
                new IdentityRole("User"){Id = "c3d25ed0-67fb-40ad-b0dc-ea4ba9a98350"}
            });

            builder.Entity<ApplicationUser>().HasData(SeedUsers());
            builder.Entity<IdentityUserRole<string>>().HasData(SeedUsersRoles());
            builder.Entity<Artwork>().HasData(SeedArtworks());
            builder.Entity<Images>().HasData(SeedImages());
        }

        private List<ApplicationUser> SeedUsers()
        {
            List<ApplicationUser> users = new();
            ApplicationUser admin = new()
            {
                Id = "6101764e-855a-461f-94f4-62c7a3527ea5",
                Email = "admin@arttales.com",
                NormalizedEmail = "ADMIN@ARTTALES.COM",
                EmailConfirmed = true,
                UserName = "admin@arttales.com",
                AppUsername = "Admin",
                NormalizedUserName = "ADMIN@ARTTALES.COM",
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ProfilePic = SeedImages()[7].Image,
                Disabled = false
            };

            admin.PasswordHash = new PasswordHasher<ApplicationUser>()
                .HashPassword(admin, "Test123`");

            ApplicationUser user = new()
            {
                Id = "268000f5-3e54-4651-aa2c-fdac814246bf",
                Email = "test@test.com",
                NormalizedEmail = "TEST@TEST.COM",
                EmailConfirmed = true,
                UserName = "test@test.com",
                AppUsername = "Test User",
                NormalizedUserName = "TEST@TEST.COM",
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ProfilePic = SeedImages()[0].Image,
                Disabled = false
            };

            user.PasswordHash = new PasswordHasher<ApplicationUser>()
                .HashPassword(user, "Test123`");

            users.Add(user);
            users.Add(admin);

            return users;

        }

        private List<Images> SeedImages()
        {
            List<Images> images = new();
            Images images1 = new()
            {
                Id = 1,
                Image = "savathun1.jpg",
                ArtworkId = 2
            };
            Images images2 = new()
            {
                Id = 2,
                Image = "savathun2.jpg",
                ArtworkId = 2
            };
            Images images3 = new()
            {
                Id = 3,
                Image = "savathun3.jpg",
                ArtworkId = 2
            };
            Images images4 = new()
            {
                Id = 4,
                Image = "rhulk1.jpg",
                ArtworkId = 3
            };
            Images images5 = new()
            {
                Id = 5,
                Image = "rhulk2.jpg",
                ArtworkId = 3
            };
            Images images6 = new()
            {
                Id = 6,
                Image = "worrior1.jpg",
                ArtworkId = 1
            };
            Images images7 = new()
            {
                Id = 7,
                Image = "worrior2.jpg",
                ArtworkId = 1
            };
            Images images8 = new()
            {
                Id = 8,
                Image = "wraith1.jpg",
                ArtworkId = 4
            };
            Images images9 = new()
            {
                Id = 9,
                Image = "wraith2.jpg",
                ArtworkId = 4
            };

            images.Add(images1);
            images.Add(images2);
            images.Add(images3);
            images.Add(images4);
            images.Add(images5);
            images.Add(images6);
            images.Add(images7);
            images.Add(images8);
            images.Add(images9);
            return images;
        }

        private List<Artwork> SeedArtworks()
        {
            List<Artwork> artworks = new();

            Artwork artwork1User = new()
            {
                Id = 1,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                Name = "Eillona",
                Likes = 6,
                Headline = "Sci Fi Portrait",
                UserId = SeedUsers()[1].Id,
                PreviewImage = 2
            };
            Artwork artwork2User = new()
            {
                Id = 2,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                Name = "Savathun",
                Likes = 7,
                Headline = "The Witch Queen",
                UserId = SeedUsers()[1].Id,
                PreviewImage = 1
            };

            Artwork artwork1Admin = new()
            {
                Id = 3,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                Name = "Rhulk",
                Likes = 5,
                Headline = "Disciple of the Witness",
                UserId = SeedUsers()[0].Id,
                PreviewImage = 2
            };
            Artwork artwork2Admin = new()
            {
                Id = 4,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                Name = "Wraith",
                Likes = 7,
                Headline = "Void Walker",
                UserId = SeedUsers()[0].Id,
                PreviewImage = 1
            };


            artworks.Add(artwork1User);
            artworks.Add(artwork2User);
            artworks.Add(artwork1Admin);
            artworks.Add(artwork2Admin);
            return artworks;
        }

        private List<IdentityUserRole<string>> SeedUsersRoles()
        {
            List<IdentityUserRole<string>> identityUserRoles = new();
            IdentityUserRole<string> roleAdmin = new() { RoleId = "78f77ce2-4753-4f0b-9826-5dab25018afa", UserId = "6101764e-855a-461f-94f4-62c7a3527ea5" };
            IdentityUserRole<string> roleUser = new() { RoleId = "c3d25ed0-67fb-40ad-b0dc-ea4ba9a98350", UserId = "268000f5-3e54-4651-aa2c-fdac814246bf" };
            identityUserRoles.Add(roleAdmin);
            identityUserRoles.Add(roleUser);
            return identityUserRoles;
        }

        public DbSet<Artwork> Artworks { get; set; }
    }
}