using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class SalaryPackage
{
    public int SalaryPackageId { get; set; }

    public string? SalaryPackageName { get; set; }

    public bool? IsActive { get; set; }

    public string? Narration { get; set; }

    public decimal? TotalAmount { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
