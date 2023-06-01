using System;
using System.Collections.Generic;

namespace Northwind.Models;

public partial class Brewery
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Beer> Beers { get; set; } = new List<Beer>();
}
