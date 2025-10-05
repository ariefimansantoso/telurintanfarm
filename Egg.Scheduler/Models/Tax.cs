using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class Tax
{
    public int TaxId { get; set; }

    public string TaxName { get; set; } = null!;

    public decimal Rate { get; set; }

    public bool IsActive { get; set; }

    public int CompanyId { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
