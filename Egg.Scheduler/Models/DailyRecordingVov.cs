using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class DailyRecordingVov
{
    public long Id { get; set; }

    public string CageNumber { get; set; } = null!;

    public string StrainName { get; set; } = null!;

    public DateTime RecordDate { get; set; }

    public decimal HenAgeWeeks { get; set; }

    public decimal HenAgeDays { get; set; }

    public int VovproductId { get; set; }

    public string VovproductName { get; set; } = null!;

    public decimal Usage { get; set; }

    public int Day { get; set; }

    public DateTime DateCreated { get; set; }

    public int CreatedBy { get; set; }
}
