using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class PayHead
{
    public int PayHeadId { get; set; }

    public string? PayHeadName { get; set; }

    public string? Type { get; set; }

    public string? Narration { get; set; }
}
