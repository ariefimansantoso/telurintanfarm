using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class RecordNotum
{
    public int Id { get; set; }

    public string JenisTelur { get; set; } = null!;

    public decimal BeratTelur { get; set; }

    public DateTime DateCreated { get; set; }

    public int CreatedBy { get; set; }

    public string NomorNota { get; set; } = null!;
}
