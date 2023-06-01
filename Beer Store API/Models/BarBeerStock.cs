using System;
using System.Collections.Generic;

namespace Northwind.Models;

public partial class BarBeerStock
{
    public long Id { get; set; }

    public long? ProductId { get; set; }

    public long? AvailableStock { get; set; }

    public long? BarId { get; set; }

    public virtual Bar? Bar { get; set; }

    public virtual Beer? Product { get; set; }
}
