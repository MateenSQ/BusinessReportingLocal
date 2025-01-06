using System;
using System.Collections.Generic;

namespace BusinessReportingMVC.Models;

public partial class BusinessDevelopment
{
    public long BusinessDevelopmentId { get; set; }

    public long BusinessDevelopmentValueId { get; set; }

    public long BusinessDevelopmentNotesId { get; set; }

    public virtual BusinessDevelopmentNote BusinessDevelopmentNotes { get; set; } = null!;

    public virtual BusinessDevelopmentValue BusinessDevelopmentValue { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
