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



namespace BusinessReportingMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IBusinessReportingRepository _repo;
        private readonly IAuthService _authService;

        public AccountController(IBusinessReportingRepository Repo, IAuthService AuthService)
        {
            _repo = Repo;
            _authService = AuthService;
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
        public async Task<IActionResult> PersonalInformation(string stringId) // Can't set claims as ints so was set to string. Needs to be parsed to int to be used in repo
        {
            long receivedID = long.Parse(stringId);

            long userIDClaim = _authService.GetIDClaim();

            if (receivedID != userIDClaim)
            {
                return Unauthorized();
            }

            // || Plan

            /*
            - Create a new view model, "personal info" or so that has fields for all information to be displayed on the page
            - Create new method in the auth service to fetch all the data and map it onto a view model which gets returned here
            - Hand the view model into the view
            - Display the user's information in the view using razor syntax
            */

            PersonalInfoViewModel personalInfo = await _authService.GetPersonalInfoAsync(userIDClaim);

            return View(personalInfo);
        }
    }
}
