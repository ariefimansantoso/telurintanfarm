using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class InvoiceSetting
{
    public int VoucherTypeId { get; set; }

    public string VoucherTypeName { get; set; } = null!;

    public string? TypeOfVoucher { get; set; }

    public int StartIndex { get; set; }

    public string Prefix { get; set; } = null!;

    public string Suffix { get; set; } = null!;

    public int CompanyId { get; set; }

    public string Ponumber { get; set; } = null!;

    public string EwayBillNo { get; set; } = null!;

    public string VechileNo { get; set; } = null!;

    public bool ShowDiscount { get; set; }

    public bool ShowTax { get; set; }

    public bool ShowBarcode { get; set; }

    public bool IsActive { get; set; }
}
