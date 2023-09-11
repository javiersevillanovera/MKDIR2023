using System;
using System.Collections.Generic;

namespace MKDIR.Domain;

public partial class BillingStatus
{
    public int BillingStatusId { get; set; }

    public int DocumentTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual DocumentType DocumentType { get; set; } = null!;
}
