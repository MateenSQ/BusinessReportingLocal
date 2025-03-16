using BusinessReportingMVC.Repositories;
using BusinessReportingMVC.Services;
using BusinessReportingMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusinessReportingMVC.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService AdminService) 
        {
            _adminService = AdminService;
        }

        public IActionResult Index()
        {
            // Reroute
            return View();
        }

        public async Task<IActionResult> ManageUsers()
        {
            /*
            Get all non-admin users in a list and display it to the front end
                - New service method
                    - new repo method to get all users where role is not admin
                    - Iterate through all and map them onto List<personalInfoViewModel> for front end use
                    - Pass List to view for strong typed views
            */

            return View(await _adminService.GetAllNonAdmins());
        }

        public async Task<IActionResult> EditUser(long id)
        {
            /*
            - Find individual user and claims from ID. X
            - Map it to a view model and send it to the front end X
            - Create a form that enables the user to change their claims
            - Create HTTP post method to receive the changes and update the database accordingly
            - reroute back to the manage users page (if possible)
            */

            PersonalInfoViewModel userToBeEdited = await _adminService.GetUserAndMapToViewModel(id);

            return View(userToBeEdited);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(PersonalInfoViewModel editedUser)
        {
            await _adminService.UpdateUserInformation(editedUser);

            return Redirect("../ManageUsers");
        }
    }
}
