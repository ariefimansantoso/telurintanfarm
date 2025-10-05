using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class Holiday
{
    public int HolidayId { get; set; }

    public DateTime? Date { get; set; }

    public string? HolidayName { get; set; }

    public string? Narration { get; set; }
}
