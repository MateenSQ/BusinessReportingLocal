using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusinessReportingMVC.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            // Reroute
            return View();
        }

        public IActionResult ManageUsers()
        {
            return View();
        }
    }
}
