using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class BudgetMaster
{
    public int BudgetMasterId { get; set; }

    public string? BudgetName { get; set; }

    public string? Type { get; set; }

    public decimal? TotalDr { get; set; }

    public decimal? TotalCr { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string? Narration { get; set; }
}
