namespace BusinessReportingMVC.ViewModels
{
    public class ProjectsViewModel
    {
        public decimal? ForecastOverallForecast { get; set; }

        public decimal? ForecastOverallDeviation { get; set; }

        public List<ProjectIndividualViewModel> ProjectIndividual { get; set; } = null!;
    }
}
