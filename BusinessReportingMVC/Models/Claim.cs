using System;
using System.Collections.Generic;

namespace BusinessReportingMVC.Models;

public partial class Claim
{
    public long ClaimId { get; set; }

    public string? ClaimType { get; set; }

    public string ClaimName { get; set; } = null!;

    public virtual ICollection<UserClaim> UserClaims { get; set; } = new List<UserClaim>();
}
