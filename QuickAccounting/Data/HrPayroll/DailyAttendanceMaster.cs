using QuickAccounting.Data.Inventory;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickAccounting.Data.HrPayroll
{
    public class DailyAttendanceMaster
    {
        [Key]
        public int DailyAttendanceMasterId { get; set; }
        public DateTime Date { get; set; }
        public string Narration { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int EmployeeID { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string PhotoSelfie { get; set; }
        public string AttendanceStatus { get; set; }
    }
}
