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

    public class Payroll
    {
        [Key]
        public int ID { get; set; }

        public int CutoffID { get; set; }

        public int EmployeeID { get; set; }

        public int WorkingDays { get; set; }

        public int FreeDays { get; set; }

        public int DayOffDays { get; set; }

        public int SickDays { get; set; }

        public int AlphaDays { get; set; }

        public decimal MonthSalary { get; set; }

        public decimal BPJS_KES { get; set; }

        public decimal BPJS_TK { get; set; }

        public decimal WorkPremi { get; set; }
               
        public decimal Penalty { get; set; }
               
        public decimal Installment { get; set; }
               
        public decimal TakeHomePay { get; set; }

        public int reatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }

    public class PayrollCutoff
    {
        [Key]
        public int ID { get; set; }

        public DateTime PayrollDate { get; set; }

        public DateTime DateCreated { get; set; }

        public string PayrollPeriode { get; set; }

        public int CreatedBy { get; set; }

        public decimal PayrollTotal { get; set; }
    }
}
