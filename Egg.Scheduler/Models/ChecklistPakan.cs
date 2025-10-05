using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class ChecklistPakan
{
    public long Id { get; set; }

    public string JenisCentrat { get; set; } = null!;

    public int Jagung { get; set; }

    public int Katul { get; set; }

    public int Centrat { get; set; }

    public DateTime DateCreated { get; set; }

    public int CreatedBy { get; set; }

    public int VerifiedBy { get; set; }

    public DateTime VerifiedOn { get; set; }
}
