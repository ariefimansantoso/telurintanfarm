using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class JournalDetail
{
    public int JournalDetailsId { get; set; }

    public int? JournalMasterId { get; set; }

    public int? LedgerId { get; set; }

    public decimal? Credit { get; set; }

    public decimal? Debit { get; set; }

    public string? Narration { get; set; }
}
