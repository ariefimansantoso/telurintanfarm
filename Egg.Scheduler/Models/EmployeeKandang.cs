using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class EmployeeKandang
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public string NomorKandang { get; set; } = null!;
}
