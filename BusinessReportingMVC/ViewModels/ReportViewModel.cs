using BusinessReportingMVC.Models;
using System;

namespace BusinessReportingMVC.ViewModels
{
    public class ReportViewModel
    {
        public long ReportId { get; set; }

        public string? ReportName { get; set; }

        public DateTime? FromDateRange { get; set; }

        public DateTime? ToDateRange { get; set; }

        public bool IsDraft { get; set; }

        public KeyHighlightsViewModel KeyHighlights { get; set; } = null!;

        public FinancialsViewModel Financials { get; set; } = null!;

        public AdminAndResourceViewModel AdminAndResources { get; set; } = null!;

        public ProjectsViewModel Projects { get; set; } = null!;

        public StrategyViewModel Strategy { get; set; } = null!;

        public BusinessDevelopmentViewModel BusinessDevelopment { get; set; } = null!;
    }
}