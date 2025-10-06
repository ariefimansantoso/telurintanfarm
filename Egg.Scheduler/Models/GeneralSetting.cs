using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class GeneralSetting
{
    public int GeneralId { get; set; }

    public string? ShowCurrency { get; set; }

    public string? NegativeCash { get; set; }

    public string? NegativeStock { get; set; }

    public string? StockCalculationMode { get; set; }

    public string? CreditLimit { get; set; }

    public string? Discount { get; set; }

    public string? VatOnPurchase { get; set; }

    public string? VatOnSales { get; set; }

    public decimal? MaxTimbangan { get; set; }
}
