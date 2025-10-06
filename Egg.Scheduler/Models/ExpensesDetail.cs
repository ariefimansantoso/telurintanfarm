using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class ExpensesDetail
{
    public int ExpensesDetailsId { get; set; }

    public int? ExpensiveMasterId { get; set; }

    public int? LedgerId { get; set; }

    public decimal? Amount { get; set; }

    public string? Narration { get; set; }
}
