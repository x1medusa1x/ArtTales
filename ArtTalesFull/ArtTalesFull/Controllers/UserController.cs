using ArtTalesFull.Abstractions;
using ArtTalesFull.Entities;
using ArtTalesFull.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ArtTalesFull.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<HomeController> _logger;
        private readonly ArtworkService artworkService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserController(IUnitOfWork unitOfWork, ILogger<HomeController> logger, ArtworkService artworkService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            _logger = logger;
            this.artworkService = artworkService;
            GlobalVariables.PathName = "User";
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        [HttpGet]
        public async Task<ActionResult> GetPostPartialView(int index, string userId)
        {
            var currentUser = signInManager.IsSignedIn(User) ? (await userManager.GetUserAsync(User)).Id : "";
            return PartialView("~/Views/Modals/_PostModal.cshtml", await artworkService.GetPostAsync(index, userId, currentUser));
        }

        [HttpGet]
        public async Task<ActionResult> GetOnLoadPostPartialView(int index, string userId)
        {
            if (index == 0)
                return Ok();
            var artworks = await unitOfWork.ArtworkRepository.GetAllArtworksForUserAsync(userId);
            var currentUser = signInManager.IsSignedIn(User) ? (await userManager.GetUserAsync(User)).Id : "";
            return PartialView("~/Views/Modals/_PostModal.cshtml", await artworkService.GetPostAsync(artworks[index - 1].Id, userId, currentUser));
        }

        [HttpGet]
        public async Task<ActionResult> GetEditPostPartialView(int index)
        {
            return PartialView("~/Views/Modals/_EditPostModal.cshtml", await artworkService.EditPostAsync(index));
        }
    }
}
