using QuickAccounting.Data.HrPayroll;

namespace QuickAccounting.Repository.Interface
{
    public interface IDepositGajiService
    {
        Task<List<DepositGaji>> GetAllAsync();
        Task<DepositGaji?> GetByIdAsync(int id);
        Task<int> CreateAsync(DepositGaji model);
        Task<bool> UpdateAsync(DepositGaji model);
        Task<bool> DeleteAsync(int id);

        Task<List<DepositGaji>> GetByEmployeeAsync(int employeeId);
        Task<List<DepositGaji>> GetByEmployeeLast12MonthsAsync(int employeeId);
        Task<List<DepositGaji>> GetByEmployeeNotExpiredNotDisbursed(int employeeId);
        Task<List<DepositGaji>> GetByEmployeeExpiredNotDisbursed(int employeeId);
        Task<List<DepositGaji>> GetByEmployeeAndDisburseCode(int employeeId, string disburseCode);
        Task<int> AssignDisburseCodeToPendingAsync(int employeeId, string uniqueDisburseCode);
    }
}