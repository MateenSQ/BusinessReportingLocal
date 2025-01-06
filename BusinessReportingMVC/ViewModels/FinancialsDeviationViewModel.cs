namespace BusinessReportingMVC.ViewModels
{
    public class FinancialsDeviationViewModel
    {
        public decimal? TurnoverDeviation { get; set; }

        public decimal? DirectCostsDeviation { get; set; }

        public decimal? GrossProfitDeviation { get; set; }

        public decimal? IndirectCostsDeviation { get; set; }

        public decimal? NetProfitDeviation { get; set; }

        public decimal? WipDeviation { get; set; }

        public int? ProductionHoursDeviation { get; set; }

        public decimal? UtilisationQuarter { get; set; }

        public int? WorkInHandHoursQuarter { get; set; }

        public decimal? WorkInHandMoneyQuarter { get; set; }

        public decimal? CashPositionForecast { get; set; }
    }
}
