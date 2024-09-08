using System;
using System.Collections.Generic;

namespace CoreMvcTemplate.Models;

public partial class SubTypes
{
    public int Id { get; set; }

    public string SubTypeName { get; set; } = null!;

    public string SubTypeDescription { get; set; } = null!;

    public int MainTypeId { get; set; }

    public virtual MainType MainType { get; set; } = null!;
}
