using System;
using System.Collections.Generic;

namespace CoreMvcTemplate.Models;

public partial class MainType
{
    public int id { get; set; }

    public string name { get; set; } = null!;

    public string description { get; set; } = null!;

    public string type { get; set; } = null!;

    public double counter { get; set; }

    public virtual ICollection<SubTypes> SubTypes { get; set; } = new List<SubTypes>();
}
