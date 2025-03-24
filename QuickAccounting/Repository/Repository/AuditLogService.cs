using QuickAccounting.Data;
using Newtonsoft.Json;

namespace QuickAccounting.Repository.Repository
{
    public interface IAuditLogService
    {
        Task LogAsync(string logType, string actionType, string description, int employeeId, string employeeName);
        string SerializeObjectToString(object obj);
    }

    public class AuditLogService : IAuditLogService
    {
        private readonly ApplicationDbContext _context;

        public AuditLogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LogAsync(string logType, string actionType, string description, int employeeId, string employeeName)
        {
            var log = new AuditLog
            {
                EmployeeName = employeeName,
                CreatedDate = DateTime.Now,
                ActionType = actionType,
                ActionDescription = description,
                EmployeeID = employeeId,
                LogType = logType
            };

            _context.AuditLog.Add(log);
            await _context.SaveChangesAsync();
        }


        public string SerializeObjectToString(object obj)
        {
            return ""; JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}
