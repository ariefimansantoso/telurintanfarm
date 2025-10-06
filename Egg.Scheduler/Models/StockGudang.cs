using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class StockGudang
{
    public int Id { get; set; }

    public decimal TotalDitimbang { get; set; }

    public DateTime DateDitimbang { get; set; }

    public int CreatedBy { get; set; }

    public string EggType { get; set; } = null!;
}
