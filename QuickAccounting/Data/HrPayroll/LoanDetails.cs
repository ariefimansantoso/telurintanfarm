using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickAccounting.Data.HrPayroll
{
    public class LoanDetails
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int LoanMasterId { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        public bool Paid { get; set; }

        public DateTime PaidDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PaidAmount { get; set; }

        // Navigation
        public virtual LoanMaster LoanMaster { get; set; }
    }
}