using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class MutasiStockTelurHarian
{
    public long Id { get; set; }

    public string JenisTelur { get; set; } = null!;

    public decimal BeratTelurIn { get; set; }

    public decimal BeratTelurOut { get; set; }

    public DateTime DateCreated { get; set; }

    public int CreatedBy { get; set; }

    public string Reason { get; set; } = null!;
}
