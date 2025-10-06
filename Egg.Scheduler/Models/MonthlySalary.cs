using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class MonthlySalary
{
    public int MonthlySalaryId { get; set; }

    public DateTime? SalaryMonth { get; set; }

    public string? YearMonth { get; set; }

    public string? Narration { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
