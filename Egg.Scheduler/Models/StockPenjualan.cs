using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class StockPenjualan
{
    public int Id { get; set; }

    public string EggType { get; set; } = null!;

    public decimal StockKg { get; set; }

    public int StockButir { get; set; }

    public DateTime DateCreated { get; set; }

    public int CreatedBy { get; set; }

    public int VerifiedBy { get; set; }

    public DateTime VerifiedOn { get; set; }

    public bool IsVerified { get; set; }
}
