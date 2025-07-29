namespace QuickAccounting.Data
{
    public class StandardData
    {
        public int ID { get; set; }
        public int Days { get; set; }
        public int Weeks { get; set; }
        public decimal FI_Standard { get; set; }
        public decimal HD_Standard { get; set; }
        public decimal EMass_Standard { get; set; }
        public decimal GrBt_Standard { get; set; }
    }


    public class EmployeeKandang
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string NomorKandang { get; set; }
    }

	public class LogPopulasiAnakKandang
	{
		public int ID { get; set; }
		public int EmployeeID { get; set; }
		public DateTime RecordDate { get; set; }
		public string CageNumber { get; set; }
		public DateTime CreatedDate { get; set; }
	}

}
