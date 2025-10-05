using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class SalaryHistory
{
    public int Id { get; set; }

    public string SalaryType { get; set; } = null!;

    public DateTime DateModified { get; set; }

    public decimal Amount { get; set; }

    public int ModifiedBy { get; set; }

    public int EmployeeId { get; set; }
}
