using System.ComponentModel.DataAnnotations;

namespace QuickAccounting.Data.HrPayroll
{
    public partial class SuratPeringatan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int SpNumber { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public DateTime DateModified { get; set; }

        [Required]
        public int ModifiedBy { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public string SpDescription { get; set; } = string.Empty;

        [Required]
        public DateTime SPDate { get; set; }

        [Required]
        public string SPOfficialNumber { get; set; } = string.Empty;
    }
}