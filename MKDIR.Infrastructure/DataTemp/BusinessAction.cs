using System;
using System.Collections.Generic;

namespace MKDIR.Infrastructure.DataTemp;

public partial class BusinessAction
{
    public int BusinessActionId { get; set; }

    public string Name { get; set; } = null!;

    public string Icon { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<TransactionAction> TransactionActions { get; set; } = new List<TransactionAction>();
}
