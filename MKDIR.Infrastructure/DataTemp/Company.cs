using System;
using System.Collections.Generic;

namespace MKDIR.Infrastructure.DataTemp;

public partial class Company
{
    public int CompanyId { get; set; }

    public int OperatorId { get; set; }

    public string Code { get; set; } = null!;

    public int PersonTypeId { get; set; }

    public int IdentificationTypeId { get; set; }

    public string FiscalId { get; set; } = null!;

    public string SecoundFiscalId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone1 { get; set; } = null!;

    public string Phone2 { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<CompanyProfile> CompanyProfiles { get; set; } = new List<CompanyProfile>();

    public virtual IdentificationType IdentificationType { get; set; } = null!;

    public virtual Operator Operator { get; set; } = null!;

    public virtual PersonType PersonType { get; set; } = null!;

    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
