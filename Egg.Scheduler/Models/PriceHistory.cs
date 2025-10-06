using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class PriceHistory
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public DateTime PriceDate { get; set; }

    public decimal Price { get; set; }

    public int ProductCode { get; set; }

    public string PriceGroup { get; set; } = null!;
}
