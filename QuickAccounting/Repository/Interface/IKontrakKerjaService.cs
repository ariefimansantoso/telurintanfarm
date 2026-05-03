using QuickAccounting.Data.HrPayroll;

namespace QuickAccounting.Repository.Interface
{
    public interface IKontrakKerjaService
    {
        Task<List<KontrakKerja>> GetAllAsync();
        Task<KontrakKerja?> GetByIdAsync(int id);
        Task<int> CreateAsync(KontrakKerja model);
        Task<bool> UpdateAsync(KontrakKerja model);
        Task<bool> DeleteAsync(int id);

        Task<List<KontrakKerja>> GetContractByEmployeeIDAsync(int employeeId);
        Task<List<KontrakKerja>> GetExpiredContractsAsync();
        Task<KontrakKerja?> GetActiveContractByEmployeeIDAsync(int employeeId);
        Task<KontrakKerja> GetUnSignedContractsAsync(int employeeID);
        Task<KontrakKerja> GetLatestSignedContractsAsync(int employeeID);
    }
}