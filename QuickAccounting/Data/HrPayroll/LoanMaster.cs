using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuickAccounting.Data.HrPayroll
{
    public class LoanMaster
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        [Required]
        public decimal LoanTotal { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public int PaymentTermsInMonths { get; set; }

        public bool LoanCompleted { get; set; }

        public virtual ICollection<LoanDetails> Details { get; set; } = new List<LoanDetails>();
    }
}