using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class UnitConvertion
{
    public int UnitconversionId { get; set; }

    public int ProductId { get; set; }

    public int UnitId { get; set; }

    public decimal ConversionRate { get; set; }

    public int Quantities { get; set; }

    public int CompanyId { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
