using QuickAccounting.Data.HrPayroll;

namespace QuickAccounting.Repository.Interface
{
    public interface IHRServices
    {
        Task<bool> DeletePerijinan(Penalty model);
        Task<bool> DeletePerijinan(Perijinan model);
        Task<int> InsertPenalty(Penalty perijinan);
        Task<int> InsertPerijinan(Perijinan perijinan);
        IQueryable<Penalty> PenaltyQuery();
        IQueryable<Perijinan> PerijinanQuery();
        Task<bool> UpdatePenalty(Penalty model);
        Task<bool> UpdatePerijinan(Perijinan model);
        List<dynamic> GetPerijinanUnApproved();
        List<dynamic> GetPerijinanBySupervisorID(int supervisorID);
        List<dynamic> GetListPerijinanByEmployeeID(int employeeID);
        List<Perijinan> GetPerijinanByEmployeeIDInPeriodePayroll(int employeeID, DateTime from, DateTime to);
        List<Perijinan> GetPerijinanByEmployeeIDAndDate(int employeeID, DateTime atDate);
    }
}