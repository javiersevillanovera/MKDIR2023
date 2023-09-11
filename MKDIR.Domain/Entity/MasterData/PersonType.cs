using System;
using System.Collections.Generic;

namespace MKDIR.Domain;

public partial class PersonType
{
    public int PersonTypeId { get; set; }

    public string Name { get; set; } = null!;

    public int OperatorId { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual Operator Operator { get; set; } = null!;
}
