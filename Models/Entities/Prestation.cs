using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Entities;

public partial class Prestation
{
    public string IdPrestation { get; set; } = null!;

    public string? Code { get; set; }

    public string Maintenance { get; set; } = null!;
}
