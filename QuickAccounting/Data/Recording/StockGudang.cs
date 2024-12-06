using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickAccounting.Data.Recording
{
    public class StockGudang
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalDitimbang { get; set; }

        [Required]
        public DateTime DateDitimbang { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        [StringLength(50)]
        public string EggType { get; set; }
    }
}
