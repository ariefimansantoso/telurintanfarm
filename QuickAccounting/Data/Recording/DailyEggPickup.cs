using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickAccounting.Data.Recording
{
    public class DailyEggPickup
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string CageNumber { get; set; }

        [Required]
        [MaxLength(250)]
        public string StrainName { get; set; }

        [Required]
        public DateTime RecordDate { get; set; }

        [Required]
        public int TelurUtuhButir { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TelurUtuhKG { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TelurUtuhNettKG { get; set; }

        [Required]
        public int TelurBentesButir { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TelurBentesKG { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TelurBentesNettKG { get; set; }

        [Required]
        public int TotalButir { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalNettKG { get; set; }

        [Required]
        public int ReceivedBy { get; set; }

        [Required]
        public DateTime ReceivedOn { get; set; }

        [Required]
        public int VerifiedBy { get; set; }

        [Required]
        public DateTime VerifiedOn { get; set; }

        [Required]
        public int CreatedBy { get; set; }
    }
}
