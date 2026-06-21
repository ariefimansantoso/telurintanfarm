using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickAccounting.Data.HrPayroll
{
    public class DepositGaji
    {
        [Key]
        public int ID { get; set; }

        public int EmployeeID { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DepositAmount { get; set; }

        public DateTime DateCreated { get; set; }

        public int CreatedBy { get; set; }

        public bool IsCredited { get; set; }

        [MaxLength(250)]
        public string DisburseCode { get; set; }
    }
}