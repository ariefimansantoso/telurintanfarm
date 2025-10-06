using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class TaskPriority
{
    public int TaskpriorityId { get; set; }

    public string? Name { get; set; }

    public string? Color { get; set; }

    public string? Description { get; set; }
}
