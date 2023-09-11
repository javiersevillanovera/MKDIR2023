using System;
using System.Collections.Generic;

namespace MKDIR.Infrastructure.DataTemp;

public partial class DocumentType
{
    public int DocumentTypeId { get; set; }

    public string Name { get; set; } = null!;

    public int OperatorId { get; set; }

    public virtual ICollection<BillingStatus> BillingStatuses { get; set; } = new List<BillingStatus>();

    public virtual ICollection<DeliveryStatus> DeliveryStatuses { get; set; } = new List<DeliveryStatus>();

    public virtual ICollection<DocumentStatus> DocumentStatuses { get; set; } = new List<DocumentStatus>();

    public virtual Operator Operator { get; set; } = null!;

    public virtual ICollection<PaymentStatus> PaymentStatuses { get; set; } = new List<PaymentStatus>();
}
