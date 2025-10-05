using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class DailySalaryVoucherDetail
{
    public int DailySalaryVoucherDetailsId { get; set; }

    public int? DailySalaryVoucherMasterId { get; set; }

    public int? EmployeeId { get; set; }

    public decimal? Wage { get; set; }

    public string? Status { get; set; }
}
