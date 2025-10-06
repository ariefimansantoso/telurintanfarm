using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class LedgerPosting
{
    public int LedgerPostingId { get; set; }

    public DateTime Date { get; set; }

    public string NepaliDate { get; set; } = null!;

    public int VoucherTypeId { get; set; }

    public string VoucherNo { get; set; } = null!;

    public int LedgerId { get; set; }

    public decimal Debit { get; set; }

    public decimal Credit { get; set; }

    public int DetailsId { get; set; }

    public int YearId { get; set; }

    public string InvoiceNo { get; set; } = null!;

    public string ChequeNo { get; set; } = null!;

    public string ChequeDate { get; set; } = null!;

    public int CompanyId { get; set; }

    public string ReferenceN { get; set; } = null!;

    public string LongReference { get; set; } = null!;

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
