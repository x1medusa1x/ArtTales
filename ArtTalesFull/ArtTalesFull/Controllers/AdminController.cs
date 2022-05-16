using ArtTalesFull.Abstractions;
using ArtTalesFull.Entities;
using ArtTalesFull.Models;
using ArtTalesFull.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ArtTalesFull.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly AdminService adminService;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AdminController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, AdminService adminService, SignInManager<ApplicationUser> signInManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.adminService = adminService;
            this.signInManager = signInManager;
            GlobalVariables.PathName = "Management";
        }
        public async Task<IActionResult> Index()
        {
            if (signInManager.IsSignedIn(User))
            {
                var user = (await userManager.GetUserAsync(User)).Id;
                if (user != "6101764e-855a-461f-94f4-62c7a3527ea5")
                    return RedirectToAction("Index", "Home");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetEditUserPartialView(string id)
        {

            return PartialView("~/Views/Modals/_EditUserModal.cshtml", await adminService.GetEditUserAsync(id));

        }

        [HttpGet]
        public async Task<ActionResult> GetDeleteUserPartialView(string id)
        {

            return PartialView("~/Views/Modals/_DeleteUserModal.cshtml", await adminService.GetDeleteUserAsync(id));

        }

        [HttpPost]
        public async Task<IActionResult> EditUser([FromForm] EditUserModel editUserModel)
        {
            await adminService.EditUser(editUserModel);

            return Redirect("/Admin");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser([FromForm] EditUserModel editUserModel)
        {
            await adminService.DeleteUser(editUserModel);

            return Redirect("/Admin");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}