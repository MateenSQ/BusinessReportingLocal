using System;
using System.Collections.Generic;

namespace BusinessReportingMVC.Models;

public partial class ProjectIndividual
{
    public long ProjectIndividualId { get; set; }

    public long ProjectId { get; set; }

    public int? ProjectCode { get; set; }

    public string? ProjectName { get; set; }

    public decimal? ForecastProfit { get; set; }

    public decimal? Deviation { get; set; }

    public bool? IsBottom { get; set; }

    public byte? Position { get; set; }

    public virtual Project Project { get; set; } = null!;
}
