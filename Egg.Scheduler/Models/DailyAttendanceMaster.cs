using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class DailyAttendanceMaster
{
    public int DailyAttendanceMasterId { get; set; }

    public DateTime? Date { get; set; }

    public string? Narration { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public int? EmployeeId { get; set; }

    public string? Lat { get; set; }

    public string? Long { get; set; }

    public string? PhotoSelfie { get; set; }

    public string? AttendanceStatus { get; set; }
}
