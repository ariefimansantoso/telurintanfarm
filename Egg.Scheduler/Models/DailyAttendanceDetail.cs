using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class DailyAttendanceDetail
{
    public int DailyAttendanceDetailsId { get; set; }

    public int? DailyAttendanceMasterId { get; set; }

    public int? EmployeeId { get; set; }

    public string? Status { get; set; }

    public string? Narration { get; set; }
}
