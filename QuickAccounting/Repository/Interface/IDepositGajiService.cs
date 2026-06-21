using QuickAccounting.Data.HrPayroll;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickAccounting.Repository.Interface
{
    public interface IDepositGajiService
    {
        Task<DepositGaji> GetByIdAsync(int id);
        Task<List<DepositGaji>> GetAllAsync();
        Task<int> AddAsync(DepositGaji entity);
        Task<bool> UpdateAsync(DepositGaji entity);
        Task<bool> DeleteAsync(int id);

        // returns all active (not yet credited) deposits for an employee
        Task<List<DepositGaji>> GetActiveDepositByEmployeeIdAsync(int employeeId);

        // marks all IsCredited = false rows as true and sets DisburseCode to a single GUID; returns the disburse code (or null if nothing to disburse)
        Task<string> DisburseDepositAsync(string disburseCode = null);
    }
}