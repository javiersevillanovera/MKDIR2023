using System;
using System.Collections.Generic;

namespace MKDIR.Infrastructure.DataTemp;

public partial class City
{
    public int CityId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int DepartmentId { get; set; }

    public bool IsActive { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
