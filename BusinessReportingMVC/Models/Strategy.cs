using System;
using System.Collections.Generic;

namespace BusinessReportingMVC.Models;

public partial class Strategy
{
    public long StrategyId { get; set; }

    public string? BusinessDevelopment { get; set; }

    public string? Innovation { get; set; }

    public string? Other { get; set; }

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
