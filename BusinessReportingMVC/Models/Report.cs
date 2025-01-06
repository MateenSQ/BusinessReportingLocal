using System;
using System.Collections.Generic;

namespace BusinessReportingMVC.Models;

public partial class Report
{
    public long ReportId { get; set; }

    public long CreatedByUserId { get; set; }

    public string? ReportName { get; set; }

    public DateTime? FromDateRange { get; set; }

    public DateTime? ToDateRange { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool IsDraft { get; set; }

    public long KeyHighlightsId { get; set; }

    public long FinancialsId { get; set; }

    public long BusinessDevelopmentId { get; set; }

    public long StrategyId { get; set; }

    public long ProjectId { get; set; }

    public long AdminAndResourcesId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual AdminAndResource AdminAndResources { get; set; } = null!;

    public virtual BusinessDevelopment BusinessDevelopment { get; set; } = null!;

    public virtual User CreatedByUser { get; set; } = null!;

    public virtual Financial Financials { get; set; } = null!;

    public virtual KeyHighlight KeyHighlights { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public virtual Strategy Strategy { get; set; } = null!;
}
