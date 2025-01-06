using System;
using System.Collections.Generic;

namespace BusinessReportingMVC.Models;

public partial class Project
{
    public long ProjectId { get; set; }

    public decimal? ForecastOverallForecast { get; set; }

    public decimal? ForecastOverallDeviation { get; set; }

    public virtual ICollection<ProjectIndividual> ProjectIndividuals { get; set; } = new List<ProjectIndividual>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
