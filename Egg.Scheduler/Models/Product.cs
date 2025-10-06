using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductCode { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public int GroupId { get; set; }

    public int BrandId { get; set; }

    public int UnitId { get; set; }

    public int TaxId { get; set; }

    public decimal PurchaseRate { get; set; }

    public decimal SalesRate { get; set; }

    public decimal Mrp { get; set; }

    public string Narration { get; set; } = null!;

    public int QtyAlert { get; set; }

    public bool IsActive { get; set; }

    public string Barcode { get; set; } = null!;

    public string Image { get; set; } = null!;

    public int CompanyId { get; set; }

    public int OpeningStock { get; set; }

    public DateTime ExiparyDate { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public int QueueNumber { get; set; }
}
