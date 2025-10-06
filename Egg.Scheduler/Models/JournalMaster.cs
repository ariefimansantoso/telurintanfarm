using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class JournalMaster
{
    public int JournalMasterId { get; set; }

    public string? SerialNo { get; set; }

    public string? VoucherNo { get; set; }

    public string? InvoiceNo { get; set; }

    public DateTime? Date { get; set; }

    public decimal? Amount { get; set; }

    public string? Narration { get; set; }

    public string? UserId { get; set; }

    public int? VoucherTypeId { get; set; }

    public int? FinancialYearId { get; set; }

    public int? CompanyId { get; set; }

    public string? Status { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? Extra2 { get; set; }
}
