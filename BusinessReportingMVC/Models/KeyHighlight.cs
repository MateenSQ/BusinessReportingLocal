using System;
using System.Collections.Generic;

namespace BusinessReportingMVC.Models;

public partial class KeyHighlight
{
    public long KeyHighlightsId { get; set; }

    public string? Performance { get; set; }

    public string? Risks { get; set; }

    public string? Opportunities { get; set; }

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
