using QuickAccounting.Data.Inventory;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickAccounting.Data.AccountModel
{
    public class JournalMaster
    {
        [Key]
        public int JournalMasterId { get; set; }
        public string SerialNo { get; set; } = string.Empty;
        public string VoucherNo { get; set; } = string.Empty;
        public string InvoiceNo { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public int VoucherTypeId { get; set; }
        public int FinancialYearId { get; set; }
        public int CompanyId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        [NotMapped]
        public List<JournalDetails> listOrder { get; set; } = new List<JournalDetails>();
        [NotMapped]
        public List<DeleteItem> listDelete { get; set; } = new List<DeleteItem>();
    }
}
