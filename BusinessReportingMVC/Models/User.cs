using System;
using System.Collections.Generic;

namespace BusinessReportingMVC.Models;

public partial class User
{
    public long UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public bool IsApproved { get; set; }

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<UserClaim> UserClaims { get; set; } = new List<UserClaim>();
}
