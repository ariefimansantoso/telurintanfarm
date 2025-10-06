using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class Company
{
    public int CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int CurrencyId { get; set; }

    public int FinancialYearId { get; set; }

    public int NoofDecimal { get; set; }

    public string Website { get; set; } = null!;

    public int WarehouseId { get; set; }

    public string Logo { get; set; } = null!;

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
