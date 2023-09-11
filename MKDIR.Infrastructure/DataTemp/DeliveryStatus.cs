using System;
using System.Collections.Generic;

namespace MKDIR.Infrastructure.DataTemp;

public partial class DeliveryStatus
{
    public int DeliveryStatusId { get; set; }

    public int DocumentTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual DocumentType DocumentType { get; set; } = null!;
}
