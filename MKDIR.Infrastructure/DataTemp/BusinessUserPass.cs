using System;
using System.Collections.Generic;

namespace MKDIR.Infrastructure.DataTemp;

public partial class BusinessUserPass
{
    public int BusinessUserId { get; set; }

    public string Pass { get; set; } = null!;

    public virtual BusinessUser BusinessUser { get; set; } = null!;
}
