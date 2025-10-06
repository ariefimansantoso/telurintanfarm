using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class StandardDatum
{
    public int Id { get; set; }

    public int Days { get; set; }

    public int Weeks { get; set; }

    public decimal FiStandard { get; set; }

    public decimal HdStandard { get; set; }

    public decimal EmassStandard { get; set; }

    public decimal GrBtStandard { get; set; }
}
