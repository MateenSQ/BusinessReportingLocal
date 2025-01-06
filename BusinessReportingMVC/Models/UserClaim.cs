using System;
using System.Collections.Generic;

namespace BusinessReportingMVC.Models;

public partial class UserClaim
{
    public long UserClaimsId { get; set; }

    public long UserId { get; set; }

    public long ClaimId { get; set; }

    public virtual Claim Claim { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
