using System;
using System.Collections.Generic;

namespace MKDIR.Infrastructure.DataTemp;

public partial class Currency
{
    public int CurrencyId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();
}
