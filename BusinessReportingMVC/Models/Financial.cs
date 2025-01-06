using System;
using System.Collections.Generic;

namespace BusinessReportingMVC.Models;

public partial class Financial
{
    public long FinancialsId { get; set; }

    public long FinancialsActualId { get; set; }

    public long FinancialsDeviationId { get; set; }

    public virtual FinancialsActual FinancialsActual { get; set; } = null!;

    public virtual FinancialsDeviation FinancialsDeviation { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
