using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class FinancialYear
{
    public int FinancialYearId { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int CompanyId { get; set; }

    public string FiscalYear { get; set; } = null!;

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
