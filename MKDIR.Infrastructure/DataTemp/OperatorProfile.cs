using System;
using System.Collections.Generic;

namespace MKDIR.Infrastructure.DataTemp;

public partial class OperatorProfile
{
    public int OperatorProfileId { get; set; }

    public string Name { get; set; } = null!;

    public int OperatorId { get; set; }

    public bool IsActive { get; set; }

    public virtual Operator Operator { get; set; } = null!;

    public virtual ICollection<BusinessUser> BusinessUsers { get; set; } = new List<BusinessUser>();

    public virtual ICollection<TransactionAction> Businesses { get; set; } = new List<TransactionAction>();
}
