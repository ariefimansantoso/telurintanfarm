using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class TaskCategory
{
    public int TaskcategoryId { get; set; }

    public string? Name { get; set; }

    public string? Color { get; set; }

    public string? Description { get; set; }
}
