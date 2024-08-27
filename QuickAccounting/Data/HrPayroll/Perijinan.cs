using System.ComponentModel.DataAnnotations;

namespace QuickAccounting.Data.HrPayroll
{
    public class Perijinan
    {
        [Key]
        public int ID { get; set; }
        public int EmployeeID { get; set; }        
        public DateTime DateSubmitted { get; set; }
        public string SubmittedFor { get; set; }
        public string SubmittedDesc { get; set; }
        public string DocPhoto { get; set; }
        public DateTime ForDate { get; set; }
        public bool? IsApproved { get; set; }
        public string ApprovalDescription { get; set; }
		public DateTime ActionDate { get; set; }
		public int ActionByEmployeeID { get; set; } 
	}

    public class Penalty
    {
        [Key]
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int AdminUserID { get; set; }
        public DateTime DateSubmitted { get; set; }          
        public DateTime ForDate { get; set; }
        public string PenaltyDescription { get; set; }
	    public decimal PenaltyAmount { get; set; }
    }
}
