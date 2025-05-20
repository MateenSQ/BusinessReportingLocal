using Microsoft.AspNetCore.Mvc;
using BusinessReportingMVC.Models;
using BusinessReportingMVC.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using BusinessReportingMVC.Repositories;
using BusinessReportingMVC.ViewModels;
// Aliases
using SecurityClaim = System.Security.Claims.Claim;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.CompilerServices;



namespace BusinessReportingMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IBusinessReportingRepository _repo;
        private readonly IAuthService _authService;
        private readonly IAdminService _adminService;

        public AccountController(IBusinessReportingRepository Repo, IAuthService AuthService, IAdminService AdminService)
        {
            _repo = Repo;
            _authService = AuthService;
            _adminService = AdminService;
        }

        public IActionResult Index()
        {
            return Redirect("Account/Login");
        }


        // =================
        // || Login Methods
        // =================
        public IActionResult Login(string returnUrl = "/")
        {
            if (User.Identity.IsAuthenticated) 
            {
                return Redirect("~/Home/Index");
            }

            return View(new LoginModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            Result result = await _authService.HandleLoginAsync(model);

            if (result.IsSuccess == false)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            model.ReturnUrl = "/"; // Otherwise Throws "ArgumentException: Value cannot be null or empty. (Parameter 'localUrl')".
            return LocalRedirect(model.ReturnUrl);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("DefaultCookie");
            return LocalRedirect("/");
        }


        // ====================
        // || Register Methods
        // ====================
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("~/Home/Index");
            }

            return View(new UserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            Result result = await _authService.HandleRegisterAsync(model);

            if (result.IsSuccess == true) 
            {
                TempData["RegisterSuccess"] = result.Message;
                return View("Login");
            }

            ViewBag.Error = result.Message;
            return View();
        }

        // ==============================
        // || Account Management Methods
        // ==============================

        [Authorize(Policy = "Approved")]
        public IActionResult Manage()
        { 
            return View();
        }

        [Authorize(Policy = "Approved")]
        public async Task<IActionResult> PersonalInformation(string stringId)
        {
            long receivedID = long.Parse(stringId);

            long userIDClaim = _authService.GetIDClaim();

            if (receivedID != userIDClaim)
            {
                return Unauthorized();
            }

            PersonalInfoViewModel personalInfo = await _adminService.GetPersonalInfoAsync(userIDClaim);

            return View(personalInfo);
        }

        [Authorize(Policy = "Approved")]
        public IActionResult DeleteInfo(long Id)
        {
            return View(Id);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePersonalInfo(long Id)
        {
            // Id is being successfully picked up. Make service method 
            await _adminService.DeleteUser(Id);

            // Invoke logout method before routing to login
            await HttpContext.SignOutAsync("DefaultCookie");

            return Redirect("../Login");
        }


        // ---------------------
        // || In Memory Methods
        // ---------------------
        public async Task<IActionResult> ApproveSelf()
        {
            _authService.ApproveSelf();

            await HttpContext.SignOutAsync("DefaultCookie");

            return LocalRedirect("/");
        }

        public async Task<IActionResult> SaithRights()
        {
            _authService.SaithRights();

            await HttpContext.SignOutAsync("DefaultCookie");

            return LocalRedirect("/");
        }

        public async Task<IActionResult> SeachtRights()
        {
            _authService.SeachtRights();

            await HttpContext.SignOutAsync("DefaultCookie");

            return LocalRedirect("/");
        }

        public async Task<IActionResult> UserRights()
        {
            _authService.UserRights();

            await HttpContext.SignOutAsync("DefaultCookie");

            return LocalRedirect("/");
        }

        public async Task<IActionResult> AdminRights()
        {
            _authService.AdminRights();

            await HttpContext.SignOutAsync("DefaultCookie");

            return LocalRedirect("/");
        }
    }
}
