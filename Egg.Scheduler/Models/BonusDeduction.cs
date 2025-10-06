using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class BonusDeduction
{
    public int BonusDeductionId { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? Date { get; set; }

    public DateTime? Month { get; set; }

    public string? YearMonth { get; set; }

    public decimal? BonusAmount { get; set; }

    public decimal? DeductionAmount { get; set; }

    public string? Narration { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
