namespace BusinessReportingMVC.ViewModels
{
    public class ProjectIndividualViewModel
    {
        public int? ProjectCode { get; set; }

        public string? ProjectName { get; set; }

        public decimal? ForecastProfit { get; set; }

        public decimal? Deviation { get; set; }

        public bool? IsBottom { get; set; }

        public byte? Position { get; set; }
    }
}
