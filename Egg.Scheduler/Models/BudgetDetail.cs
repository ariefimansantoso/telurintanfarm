using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class BudgetDetail
{
    public int BudgetDetailsId { get; set; }

    public int? BudgetMasterId { get; set; }

    public int? LedgerId { get; set; }

    public decimal? Credit { get; set; }

    public decimal? Debit { get; set; }
}
