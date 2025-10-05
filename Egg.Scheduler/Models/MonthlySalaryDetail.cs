using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class MonthlySalaryDetail
{
    public int MonthlySalaryDetailsId { get; set; }

    public int? EmployeeId { get; set; }

    public int? SalaryPackageId { get; set; }

    public int? MonthlySalaryId { get; set; }
}
