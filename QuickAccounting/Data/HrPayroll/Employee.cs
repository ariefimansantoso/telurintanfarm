using System.ComponentModel.DataAnnotations;

namespace QuickAccounting.Data.HrPayroll
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select a Designation.")]
        public int DesignationId { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime Dob { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public string Qualification { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public DateTime JoiningDate { get; set; }
        public bool isActive { get; set; }
        public string Narration { get; set; }
        public string SalaryType { get; set; }
        public decimal DailyWage { get; set; }
		[Range(1, int.MaxValue, ErrorMessage = "Please select Package.")]
		public int DefaultPackageId { get; set; }
        public int UserID { get; set; }
        public string BirthPlace { get; set; }
        public string NIK { get; set; }
        public string ID_BPJS_KES { get; set; }
        public string ID_BPJS_TK { get; set; }
        public string BankAccount { get; set; }
		public int SupervisorID { get; set; }
        public decimal BPJS_KES { get; set; }
        public decimal BPJS_TK { get; set; }
        public decimal Lembur { get; set; }
        public decimal Tunjangan { get; set; }
        public string JobDesc { get; set; }
        public string SOP { get; set; }
		public int OldEmployeeId { get; set; }
	}
}
