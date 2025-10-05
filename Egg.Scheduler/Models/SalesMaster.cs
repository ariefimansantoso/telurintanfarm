using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class SalesMaster
{
    public int SalesMasterId { get; set; }

    public string SerialNo { get; set; } = null!;

    public string VoucherNo { get; set; } = null!;

    public int WarehouseId { get; set; }

    public int VoucherTypeId { get; set; }

    public DateTime Date { get; set; }

    public DateTime DueDate { get; set; }

    public int LedgerId { get; set; }

    public string? Reference { get; set; }

    public string? PaymentStatus { get; set; }

    public int OrderMasterId { get; set; }

    public int QuotationMasterId { get; set; }

    public string Narration { get; set; } = null!;

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

    public bool Pos { get; set; }

    public int FinancialYearId { get; set; }

    public int CompanyId { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public decimal Bayar { get; set; }

    public decimal Kembali { get; set; }

    public int Kp10 { get; set; }

    public int Kp15 { get; set; }

    public bool SudahDiambil { get; set; }

    public string TipePembayaran { get; set; } = null!;
}
