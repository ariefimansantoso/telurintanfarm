using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class StockTelurUtuh
{
    public int Id { get; set; }

    public decimal StockKg { get; set; }

    public int ModifiedBy { get; set; }

    public DateTime ModifiedOn { get; set; }

    public DateTime StockDate { get; set; }
}
