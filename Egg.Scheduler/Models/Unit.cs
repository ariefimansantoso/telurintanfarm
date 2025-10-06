using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class Unit
{
    public int UnitId { get; set; }

    public string UnitName { get; set; } = null!;

    public int NoOfDecimalplaces { get; set; }

    public int CompanyId { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
