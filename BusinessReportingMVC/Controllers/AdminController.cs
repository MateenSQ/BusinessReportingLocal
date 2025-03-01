using Microsoft.AspNetCore.Mvc;

namespace BusinessReportingMVC.Controllers
{
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
