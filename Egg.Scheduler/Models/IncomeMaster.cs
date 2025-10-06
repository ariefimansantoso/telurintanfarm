using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class IncomeMaster
{
    public int IncomeMasterId { get; set; }

    public int LedgerId { get; set; }

    public DateTime? Date { get; set; }

    public int VoucherTypeId { get; set; }

    public string SerialNo { get; set; } = null!;

    public string VoucherNo { get; set; } = null!;

    public decimal Amount { get; set; }

    public string Narration { get; set; } = null!;

    public int FinancialYearId { get; set; }

    public int CompanyId { get; set; }

    public string UserId { get; set; } = null!;

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
