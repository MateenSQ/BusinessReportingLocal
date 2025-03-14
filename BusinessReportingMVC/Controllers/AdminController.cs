using BusinessReportingMVC.Services;
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
    }
}
