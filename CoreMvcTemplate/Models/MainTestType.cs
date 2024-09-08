using System;
using System.Collections.Generic;

namespace CoreMvcTemplate.Models;

public partial class MainTestType
{
    public int id { get; set; }

    public string name { get; set; } = null!;

    public string description { get; set; } = null!;

    public string type { get; set; } = null!;

    public double counter { get; set; }

    public int? MainTypeId { get; set; }

    public virtual SubTestType? MainType { get; set; }
}
