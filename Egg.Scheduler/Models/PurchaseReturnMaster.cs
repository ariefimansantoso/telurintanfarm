using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class PurchaseReturnMaster
{
    public int PurchaseReturnMasterId { get; set; }

    public string SerialNo { get; set; } = null!;

    public string VoucherNo { get; set; } = null!;

    public int WarehouseId { get; set; }

    public int VoucherTypeId { get; set; }

    public DateTime Date { get; set; }

    public int LedgerId { get; set; }

    public string Narration { get; set; } = null!;

    public int PurchaseMasterId { get; set; }

    public int TaxId { get; set; }

    public decimal TotalTax { get; set; }

    public decimal TaxRate { get; set; }

    public decimal DisPer { get; set; }

    public decimal BillDiscount { get; set; }

    public decimal ShippingAmount { get; set; }

    public decimal GrandTotal { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal NetAmounts { get; set; }

    public decimal PayAmount { get; set; }

    public decimal BalanceDue { get; set; }

    public string Status { get; set; } = null!;

    public decimal PreviousDue { get; set; }

    public int PaymentId { get; set; }

    public string UserId { get; set; } = null!;

    public int FinancialYearId { get; set; }

    public int CompanyId { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
