using System;
using System.Collections.Generic;

namespace MKDIR.Infrastructure.DataTemp;

public partial class PaymentStatus
{
    public int PaymentStatusId { get; set; }

    public int DocumentTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual DocumentType DocumentType { get; set; } = null!;
}
