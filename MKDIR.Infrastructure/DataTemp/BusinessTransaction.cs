using System;
using System.Collections.Generic;

namespace MKDIR.Infrastructure.DataTemp;

public partial class BusinessTransaction
{
    public int BusinessTransactionId { get; set; }

    public int Sequence { get; set; }

    public string Name { get; set; } = null!;

    public int BusinessModuleId { get; set; }

    public string Icon { get; set; } = null!;

    public string Urlpath { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual BusinessModule BusinessModule { get; set; } = null!;

    public virtual ICollection<TransactionAction> TransactionActions { get; set; } = new List<TransactionAction>();
}
