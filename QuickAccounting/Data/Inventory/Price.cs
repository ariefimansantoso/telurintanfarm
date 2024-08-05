namespace QuickAccounting.Data.Inventory
{
	public class PriceMaster
	{
		public int ID { get; set; }
        public int ProductID { get; set; }
        public DateTime PriceDate { get; set; }
        public decimal Price { get; set; }
        public int ProductCode { get; set; }
    }
}
