using System;
using System.Collections.Generic;

namespace MKDIR.Domain;

public partial class Country
{
    public int CountryId { get; set; }

    public string Name { get; set; } = null!;

    public int CurrencyId { get; set; }

    public bool IsActive { get; set; }

    public virtual Currency Currency { get; set; } = null!;

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<Operator> Operators { get; set; } = new List<Operator>();

    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
