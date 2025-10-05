using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class PaymentDetail
{
    public int PaymentDetailsId { get; set; }

    public int PaymentMasterId { get; set; }

    public int PurchaseMasterId { get; set; }

    public int? LedgerId { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal ReceiveAmount { get; set; }

    public decimal DueAmount { get; set; }
}
