using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickAccounting.Data.Recording
{
    public class StockOpnam
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal StockAkhir { get; set; }

        public DateTime StockDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public int GroupId { get; set; }
    }
}