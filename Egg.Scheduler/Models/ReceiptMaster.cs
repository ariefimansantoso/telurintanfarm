using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class ReceiptMaster
{
    public int ReceiptMasterId { get; set; }

    public int? SalesMasterId { get; set; }

    public string VoucherNo { get; set; } = null!;

    public DateTime Date { get; set; }

    public int LedgerId { get; set; }

    public decimal Amount { get; set; }

    public string SerialNo { get; set; } = null!;

    public string Narration { get; set; } = null!;

    public int VoucherTypeId { get; set; }

    public string? Type { get; set; }

    public string UserId { get; set; } = null!;

    public string PaymentType { get; set; } = null!;

    public int FinancialYearId { get; set; }

    public int CompanyId { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
