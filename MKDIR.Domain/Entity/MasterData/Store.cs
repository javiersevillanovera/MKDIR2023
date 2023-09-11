using System;
using System.Collections.Generic;

namespace MKDIR.Domain;

public partial class Store
{
    public int StoreId { get; set; }

    public int CompanyId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int CountryId { get; set; }

    public int DepartmentId { get; set; }

    public int CityId { get; set; }

    public string Address { get; set; } = null!;

    public string Phone1 { get; set; } = null!;

    public string Phone2 { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Logo { get; set; } = null!;

    public string TimeZone { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<UserCompanyProfile> UserCompanyProfiles { get; set; } = new List<UserCompanyProfile>();
}
