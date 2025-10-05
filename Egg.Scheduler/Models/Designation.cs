using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class Designation
{
    public int DesignationId { get; set; }

    public string? DesignationName { get; set; }

    public decimal? LeaveDays { get; set; }

    public decimal? AdvanceAmount { get; set; }

    public string? Narration { get; set; }
}
