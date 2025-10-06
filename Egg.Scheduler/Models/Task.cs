using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public string? TaskName { get; set; }

    public int? TaskcategoryId { get; set; }

    public int? TaskpriorityId { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Description { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
