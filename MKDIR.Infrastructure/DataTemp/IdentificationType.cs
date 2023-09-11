using System;
using System.Collections.Generic;

namespace MKDIR.Infrastructure.DataTemp;

public partial class IdentificationType
{
    public int IdentificationTypeId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int OperatorId { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual Operator Operator { get; set; } = null!;
}
