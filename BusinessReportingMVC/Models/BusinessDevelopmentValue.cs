using System;
using System.Collections.Generic;

namespace BusinessReportingMVC.Models;

public partial class BusinessDevelopmentValue
{
    public long BusinessDevelopmentValueId { get; set; }

    public decimal? AwarenessValue { get; set; }

    public decimal? IntentValue { get; set; }

    public decimal? Tendered033Value { get; set; }

    public decimal? Tendered3466Value { get; set; }

    public decimal? Tendered67100Value { get; set; }

    public virtual ICollection<BusinessDevelopment> BusinessDevelopments { get; set; } = new List<BusinessDevelopment>();
}
