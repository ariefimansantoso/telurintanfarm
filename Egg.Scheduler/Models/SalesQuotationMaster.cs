using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class SalesQuotationMaster
{
    public int QuotationMasterId { get; set; }

    public string SerialNo { get; set; } = null!;

    public string VoucherNo { get; set; } = null!;

    public int VoucherTypeId { get; set; }

    public DateTime Date { get; set; }

    public int LedgerId { get; set; }

    public decimal TaxId { get; set; }

    public decimal TotalTax { get; set; }

    public decimal TaxRate { get; set; }

    public decimal DisPer { get; set; }

    public decimal BillDiscount { get; set; }

    public decimal ShippingAmount { get; set; }

    public decimal GrandTotal { get; set; }

    public decimal TotalAmount { get; set; }

    public string Narration { get; set; } = null!;

    public int WarehouseId { get; set; }

    public int FinancialYearId { get; set; }

    public int CompanyId { get; set; }

    public string UserId { get; set; } = null!;

    public DateTime AddedDate { get; set; }

    public DateTime ModifyDate { get; set; }
}
