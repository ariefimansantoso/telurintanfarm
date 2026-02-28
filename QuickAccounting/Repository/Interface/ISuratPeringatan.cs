using QuickAccounting.Data.HrPayroll;

namespace QuickAccounting.Repository.Interface
{
    public interface ISuratPeringatan
    {
        Task<List<SuratPeringatan>> GetAll();
        Task<SuratPeringatan> GetByID(int id);
        Task<List<SuratPeringatan>> GetByEmployeeID(int employeeId);
        Task<int> Insert(SuratPeringatan model);
        Task<bool> Update(SuratPeringatan model);
        Task<bool> SetDeleted(int id);
    }
}