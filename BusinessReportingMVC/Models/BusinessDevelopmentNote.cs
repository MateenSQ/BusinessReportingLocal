using System;
using System.Collections.Generic;

namespace BusinessReportingMVC.Models;

public partial class BusinessDevelopmentNote
{
    public long BusinessDevelopmentNotesId { get; set; }

    public string? AwarenessNotes { get; set; }

    public string? IntentNotes { get; set; }

    public string? Tendered033Notes { get; set; }

    public string? Tendered3466Notes { get; set; }

    public string? Tendered67100Notes { get; set; }

    public virtual ICollection<BusinessDevelopment> BusinessDevelopments { get; set; } = new List<BusinessDevelopment>();
}
