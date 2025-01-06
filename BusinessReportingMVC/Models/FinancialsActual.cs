using System;
using System.Collections.Generic;

namespace BusinessReportingMVC.Models;

public partial class FinancialsActual
{
    public long FinancialsActualId { get; set; }

    public decimal? TurnoverActual { get; set; }

    public decimal? DirectCostsActual { get; set; }

    public decimal? GrossProfitActual { get; set; }

    public decimal? IndirectCostsActual { get; set; }

    public decimal? NetProfitActual { get; set; }

    public decimal? WipActual { get; set; }

    public int? ProductionHoursQuarter { get; set; }

    public decimal? UtilisationQuarter { get; set; }

    public int? WorkInHandHoursQuarter { get; set; }

    public decimal? WorkInHandMoneyQuarter { get; set; }

    public decimal? CashPositionActual { get; set; }

    public virtual ICollection<Financial> Financials { get; set; } = new List<Financial>();
}
