using System;
using System.Collections.Generic;

namespace MKDIR.Domain;

public partial class CompanyProfile
{
    public int CompanyProfileId { get; set; }

    public string Name { get; set; } = null!;

    public int CompanyId { get; set; }

    public bool IsActive { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<UserCompanyProfile> UserCompanyProfiles { get; set; } = new List<UserCompanyProfile>();

    public virtual ICollection<TransactionAction> Businesses { get; set; } = new List<TransactionAction>();
}
