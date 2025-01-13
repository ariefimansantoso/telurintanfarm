namespace QuickAccounting.Data
{
    public class AuditLog
    {       
            public long ID { get; set; } // Maps to bigint
            public string EmployeeName { get; set; } // Maps to nvarchar(50)
            public DateTime CreatedDate { get; set; } // Maps to datetime2(7)
            public string ActionType { get; set; } // Maps to nvarchar(50)
            public string ActionDescription { get; set; } // Maps to nvarchar(MAX)
            public int EmployeeID { get; set; } // Maps to int       
        public string LogType { get; set; }
    }
}
