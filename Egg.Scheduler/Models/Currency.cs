using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class Currency
{
    public int CurrencyId { get; set; }

    public string CurrencySymbol { get; set; } = null!;

    public string CurrencyName { get; set; } = null!;

    public decimal NoOfDecimalPlaces { get; set; }

    public bool IsDefault { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
