using System;
using System.Collections.Generic;

namespace MKDIR.Domain;

public partial class RecoverPassword
{
    public int RequestId { get; set; }

    public int BusinessUserId { get; set; }

    public string RecoverCode { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime RequestDate { get; set; }

    public virtual BusinessUser BusinessUser { get; set; } = null!;
}
