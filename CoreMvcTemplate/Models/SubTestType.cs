using System;
using System.Collections.Generic;

namespace CoreMvcTemplate.Models;

public partial class SubTestType
{
    public int Id { get; set; }

    public string SubTypeName { get; set; } = null!;

    public string SubTypeDescription { get; set; } = null!;

    public int MainTypeId { get; set; }

    public string? AddColumn { get; set; }

    public virtual ICollection<MainTestType> MainTestType { get; set; } = new List<MainTestType>();
}
