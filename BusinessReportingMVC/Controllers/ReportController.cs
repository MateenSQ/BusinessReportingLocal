using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Net.WebRequestMethods;
using BusinessReportingMVC.Services;
using BusinessReportingMVC.Repositories;
using BusinessReportingMVC.Models;
using BusinessReportingMVC.ViewModels;
using BusinessReportingMVC.Data;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace BusinessReportingMVC.Controllers
{
    [Authorize(Policy = "Approved")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IAuthService _authService;
        private readonly IBusinessReportingRepository _repo;
        private readonly BusinessReportingDbContext _context;

        public ReportController(IReportService ReportService, IBusinessReportingRepository Repo, BusinessReportingDbContext Context, IAuthService AuthService) 
        {
            _reportService = ReportService;
            _authService = AuthService;
            _repo = Repo;
            _context = Context;
        }

        public IActionResult Index()
        {
            return Redirect("/Report/Reports");
        }

        public async Task<IActionResult> Published()
        {
            return View(await _reportService.DisplayUserPublishedReportsAsync()); 
        }

        public async Task<IActionResult> Drafts() // Always remember the awaits
        {
            try
            {
                return View(await _reportService.DisplayUserDraftReportsAsync());
            }
            catch (Exception ex)                                                   // Use this type of exception handling?
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        [Authorize(Policy = "SeachtAccess")]
        public async Task<IActionResult> SeachtPublished()
        {
            List<Report> reports = await _repo.GetListOfAllPublishedReports();

            return View(reports);
        }

        // ===============
        // || New Methods
        // ===============
        public IActionResult New() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Investigate how to use Anti Forgery?
        public async Task<IActionResult> CreateNew()
        {
            await _reportService.CreateReportAsync();

            return Redirect("Drafts");
        }

        // ===============
        // || View Method
        // ===============
        public async Task<IActionResult> View(long id)
        {
            Report selectedReport = await _repo.GetReportByIdAsync(id);

            long userId = Int64.Parse(this.User.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);

            string userPosition = this.User.Claims.First(c => c.Type == "Position").Value;

            if ((userId == selectedReport.CreatedByUserId && selectedReport.IsDraft == false) || (userPosition == "Seacht" && selectedReport.IsDraft == false))
            {
                ReportViewModel reportViewModel = await _reportService.MapReportToViewModelAsync(await _repo.GetReportByIdAsync(id));

                return View(reportViewModel);
            }
            
            return Unauthorized();
        }


        // ================
        // || Edit Methods
        // ================
        public async Task<IActionResult> Edit(long id) 
        {
            Report selectedReport = await _repo.GetReportByIdAsync(id);

            long userIDClaim = _authService.GetIDClaim();

            if (selectedReport.CreatedByUserId == userIDClaim)
            {
                ReportViewModel reportViewModel = await _reportService.MapReportToViewModelAsync(await _repo.GetReportByIdAsync(id));

                return View(reportViewModel);
            }
            
            return Unauthorized();
        }

        // Saving Reports
        [HttpPost] 
        public async Task<IActionResult> Edit(ReportViewModel reportViewModel)
        {
            using (_context)
            {
                Report report = await _repo.GetReportByIdAsync(reportViewModel.ReportId);

                await _reportService.MapViewModelToReportAsync(reportViewModel, report);

                await _context.SaveChangesAsync();
            }

            TempData["SaveSuccess"] = "Report successfully saved";

            return Redirect("../Drafts");
        }


        // ===================
        // || Publish Methods
        // ===================
        [HttpPost]
        public async Task<IActionResult> Publish(long id)
        {
            Result result = await _reportService.CheckReportIsCompleted(id);

            if (result.IsSuccess == false)
            {
                TempData["errorMessage"] = result.Message; // Need to restore model state in edit

                return RedirectToAction("Edit", new { id = id });
            }

            using (_context)
            {
                Report report = await _repo.GetReportByIdAsync(id);

                report.IsDraft = false;

                await _context.SaveChangesAsync();
            }

            // Add success message
            TempData["PublishSuccess"] = "Report Successfully published";

            return Redirect("../../Home");
        }

        // ====================
        // || 'Delete' Methods
        // ====================
        public async Task<IActionResult> Delete(long id)
        {
            SmallReportViewModel viewModel = _reportService.MapReportToSmallReport(await _repo.GetReportByIdAsync(id));

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReport(SmallReportViewModel deleteViewModel) 
        {
            using (_context)
            {
                Report report = await _repo.GetReportByIdAsync(deleteViewModel.Id);

                report.IsDeleted = true;

                await _context.SaveChangesAsync();
            }

            return Redirect("./Drafts");
        }
















        // ==============================================================
        // || COME BACK TO FOR OPTIMIZATION FINDING NAMEIDENTIFIER CLAIM
        // ==============================================================

        //var stuff = User.FindFirst(ClaimTypes.Name).Value;
        //stuff = User.Identity.Name;

        //ReportViewModel repViewModel = new ReportViewModel();
        //var id = this.User.FindFirst("sub");
        //var random = User.Claims.First().Type;

        //var gettingThere = User.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

        //var betterWay = User.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

    }
}