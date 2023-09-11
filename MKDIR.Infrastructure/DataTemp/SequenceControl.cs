using System;
using System.Collections.Generic;

namespace MKDIR.Infrastructure.DataTemp;

public partial class SequenceControl
{
    public int SequenceControlId { get; set; }

    public int OperatorId { get; set; }

    public string ObjectName { get; set; } = null!;

    public short Accuracy { get; set; }

    public int NextValue { get; set; }

    public virtual Operator Operator { get; set; } = null!;
}
