using System;
using System.Collections.Generic;

namespace MKDIR.Domain;

public partial class UserCompanyProfile
{
    public int BusinessUserId { get; set; }

    public int StoreId { get; set; }

    public int CompanyProfileId { get; set; }

    public virtual BusinessUser BusinessUser { get; set; } = null!;

    public virtual CompanyProfile CompanyProfile { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
