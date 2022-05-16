using ArtTalesFull.Abstractions;
using ArtTalesFull.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArtTalesFull.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<HomeController> _logger;
        private readonly ArtworkService artworkService;

        public UserController(IUnitOfWork unitOfWork, ILogger<HomeController> logger, ArtworkService artworkService)
        {
            this.unitOfWork = unitOfWork;
            _logger = logger;
            this.artworkService = artworkService;
            GlobalVariables.PathName = "User";
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
        public async Task<ActionResult> GetPostPartialView(int index)
        {
            return PartialView("~/Views/Modals/_PostModal.cshtml", await artworkService.GetPostAsync(index));
        }

        [HttpGet]
        public async Task<ActionResult> GetEditPostPartialView(int index)
        {
            return PartialView("~/Views/Modals/_EditPostModal.cshtml", await artworkService.EditPostAsync(index));
        }
    }
}
