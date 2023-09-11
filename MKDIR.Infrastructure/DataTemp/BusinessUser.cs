using System;
using System.Collections.Generic;

namespace MKDIR.Infrastructure.DataTemp;

public partial class BusinessUser
{
    public int BusinessUserId { get; set; }

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string SureName { get; set; } = null!;

    public bool IsOperator { get; set; }

    public bool IsActive { get; set; }

    public virtual BusinessUserPass? BusinessUserPass { get; set; }

    public virtual ICollection<RecoverPassword> RecoverPasswords { get; set; } = new List<RecoverPassword>();

    public virtual ICollection<UserCompanyProfile> UserCompanyProfiles { get; set; } = new List<UserCompanyProfile>();

    public virtual ICollection<OperatorProfile> OperatorProfiles { get; set; } = new List<OperatorProfile>();
}
