using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class SalaryVoucherMaster
{
    public int SalaryVoucherMasterId { get; set; }

    public string? LedgerId { get; set; }

    public string? SerialNo { get; set; }

    public string? VoucherNo { get; set; }

    public string? InvoiceNo { get; set; }

    public DateTime? Date { get; set; }

    public DateTime? Month { get; set; }

    public string? YearMonth { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? Narration { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public int? VoucherTypeId { get; set; }

    public int? FinancialYearId { get; set; }
}
