using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class DailyEggPickup
{
    public int Id { get; set; }

    public string CageNumber { get; set; } = null!;

    public string StrainName { get; set; } = null!;

    public DateTime RecordDate { get; set; }

    public int TelurUtuhButir { get; set; }

    public decimal TelurUtuhKg { get; set; }

    public decimal TelurUtuhNettKg { get; set; }

    public int TelurBentesButir { get; set; }

    public decimal TelurBentesKg { get; set; }

    public decimal TelurBentesNettKg { get; set; }

    public int TotalButir { get; set; }

    public decimal TotalNettKg { get; set; }

    public int ReceivedBy { get; set; }

    public DateTime ReceivedOn { get; set; }

    public int VerifiedBy { get; set; }

    public DateTime VerifiedOn { get; set; }

    public int CreatedBy { get; set; }

    public int TelurPutihButir { get; set; }

    public decimal TelurPutihKg { get; set; }

    public decimal TelurPutihNettKg { get; set; }
}
