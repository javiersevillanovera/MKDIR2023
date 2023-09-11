using System;
using System.Collections.Generic;

namespace MKDIR.Infrastructure.DataTemp;

public partial class SuplierCategory
{
    public int SuplierCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public int OperatorId { get; set; }

    public virtual Operator Operator { get; set; } = null!;
}
