using System;
using System.Collections.Generic;

namespace BusinessReportingMVC.Models;

public partial class AdminAndResource
{
    public long AdminAndResourcesId { get; set; }

    public string? PointsOfNote { get; set; }

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
