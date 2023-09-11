using System;
using System.Collections.Generic;

namespace MKDIR.Domain;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int CountryId { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
