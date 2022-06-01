using ArtTalesFull.Abstractions;
using ArtTalesFull.Entities;
using ArtTalesFull.Models;
using ArtTalesFull.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ArtTalesFull.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ArtworkService artworkService;
        private readonly SignInManager<ApplicationUser> signInManager;

        public HomeController(IUnitOfWork unitOfWork, ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, ArtworkService artworkService, SignInManager<ApplicationUser> signInManager)
        {
            this.unitOfWork = unitOfWork;
            _logger = logger;
            this.userManager = userManager;
            this.artworkService = artworkService;
            this.signInManager = signInManager;
            GlobalVariables.PathName = "Home";
        }

        [HttpGet]
        public IActionResult Index(string name = "")
        {
            var model = new SearchHome()
            {
                Name = name,
            };
            return View(model);
        }

        public IActionResult UserProfile(string id, int index)
        {
            var userProfileModel = new UserProfileModel()
            {
                id = id,
                index = index
            };
            return View(userProfileModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<ActionResult> GetUploadPartialView()
        {

            return signInManager.IsSignedIn(User) ?
                    ((await userManager.GetUserAsync(User)).Disabled == false ?
                    PartialView("~/Views/Modals/_UploadModal.cshtml", await artworkService.GetUploadAsync()) :
                    PartialView("~/Views/Modals/_DisabledUserModal.cshtml")) :
                PartialView("~/Views/Modals/_NotLoggedUploadModal.cshtml");

        }

        [HttpPost]
        public async Task<IActionResult> UploadPost([FromForm] UploadModel uploadModel)
        {
            await artworkService.AddArtwork(uploadModel, User);

            return RedirectToAction("");
        }

        [HttpPost]
        public async Task<IActionResult> EditPost([FromForm] UploadModel uploadModel)
        {
            await artworkService.EditArtwork(uploadModel, User);

            return Redirect("/Identity/Account/Manage");
        }

        [HttpPost]
        public async Task<IActionResult> LikePost([FromBody] LikeModel likeModel)
        {
            await artworkService.LikePostAsync(likeModel.ArtworkId, likeModel.Value);

            return Ok();
        }
    }
}