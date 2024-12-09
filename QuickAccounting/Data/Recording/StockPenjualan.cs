namespace QuickAccounting.Data.Recording
{
    public class StockPenjualan
    {
        public int ID { get; set; }
        public string EggType { get; set; } = string.Empty;
        public decimal StockKg { get; set; }
        public int StockButir { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public int VerifiedBy { get; set; }
        public DateTime VerifiedOn { get; set; }
        public bool IsVerified { get; set; }
    }
}
