using System;
using System.Collections.Generic;

namespace MKDIR.Domain;

public partial class BusinessModule
{
    public int BusinessModuleId { get; set; }

    public int Sequence { get; set; }

    public string Name { get; set; } = null!;

    public string Icon { get; set; } = null!;

    public bool IsOperator { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<BusinessTransaction> BusinessTransactions { get; set; } = new List<BusinessTransaction>();
}
