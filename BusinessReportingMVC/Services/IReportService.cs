using BusinessReportingMVC.Models;
using BusinessReportingMVC.ViewModels;

namespace BusinessReportingMVC.Services
{
    public interface IReportService
    {
        public Task CreateReportAsync();

        public Task<List<Report>> DisplayUserDraftReportsAsync();

        public Task<ReportViewModel> MapReportToViewModelAsync(Report report);

        public Task<ReportViewModel> DisplayDraftReportAsync(int id);

        public Task<List<Report>> DisplayUserPublishedReportsAsync();

        public Task<Report> MapViewModelToReportAsync(ReportViewModel viewModel, Report report);

        public SmallReportViewModel MapReportToSmallReport(Report report);

        public Task<Result> CheckReportIsCompleted(long id);
    }
}
