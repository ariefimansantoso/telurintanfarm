using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class PayrollCutoff
{
    public int Id { get; set; }

    public DateTime PayrollDateStart { get; set; }

    public DateTime PayrollDate { get; set; }

    public DateTime DateCreated { get; set; }

    public string PayrollPeriode { get; set; } = null!;

    public int CreatedBy { get; set; }

    public decimal PayrollTotal { get; set; }

    public DateTime RealPayrollDate { get; set; }
}
