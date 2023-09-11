using System;
using System.Collections.Generic;

namespace MKDIR.Domain;

public partial class Operator
{
    public int OperatorId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int CountryId { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<DocumentType> DocumentTypes { get; set; } = new List<DocumentType>();

    public virtual ICollection<IdentificationType> IdentificationTypes { get; set; } = new List<IdentificationType>();

    public virtual ICollection<OperatorProfile> OperatorProfiles { get; set; } = new List<OperatorProfile>();

    public virtual ICollection<PersonType> PersonTypes { get; set; } = new List<PersonType>();

    public virtual ICollection<SequenceControl> SequenceControls { get; set; } = new List<SequenceControl>();

    public virtual ICollection<SuplierCategory> SuplierCategories { get; set; } = new List<SuplierCategory>();
}
