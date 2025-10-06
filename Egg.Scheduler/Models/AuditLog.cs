using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class AuditLog
{
    public long Id { get; set; }

    public string EmployeeName { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ActionType { get; set; } = null!;

    public string ActionDescription { get; set; } = null!;

    public int EmployeeId { get; set; }

    public string LogType { get; set; } = null!;
}
