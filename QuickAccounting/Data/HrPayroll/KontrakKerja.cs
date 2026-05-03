using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickAccounting.Data.HrPayroll
{
    public class KontrakKerja
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public DateTime DateStart { get; set; }

        [Required]
        public DateTime DateEnd { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        public string ContractDetails { get; set; }

        public DateTime DateCreated { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? DateModified { get; set; }

        public int? ModifiedBy { get; set; }

        public bool IsExpired { get; set; }

        public bool IsPermanent { get; set; }

        public bool IsSigned { get; set; }
    }
}