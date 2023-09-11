using System;
using System.Collections.Generic;

namespace MKDIR.Infrastructure.DataTemp;

public partial class TransactionAction
{
    public int BusinessTransactionId { get; set; }

    public int BusinessActionId { get; set; }

    public virtual BusinessAction BusinessAction { get; set; } = null!;

    public virtual BusinessTransaction BusinessTransaction { get; set; } = null!;

    public virtual ICollection<CompanyProfile> CompanyProfiles { get; set; } = new List<CompanyProfile>();

    public virtual ICollection<OperatorProfile> OperatorProfiles { get; set; } = new List<OperatorProfile>();
}
