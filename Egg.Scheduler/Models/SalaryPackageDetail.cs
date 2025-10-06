using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class SalaryPackageDetail
{
    public int SalaryPackageDetailsId { get; set; }

    public int? SalaryPackageId { get; set; }

    public int? PayHeadId { get; set; }

    public decimal? Amount { get; set; }

    public string? Narration { get; set; }
}
