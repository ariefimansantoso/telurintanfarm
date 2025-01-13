using QuickAccounting.Data.HrPayroll;

namespace QuickAccounting.Repository.Interface
{
    public interface IHRServices
    {
        Task<bool> DeletePenalty(Penalty model);
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
        List<AbsensiPotongan> GetByCurrentMonthYearAndEmployeeId(int employeeId, int month, int year);
        List<Penalty> GetPenaltyByCurrentMonthYearAndEmployeeId(int employeeId, DateTime from, DateTime to);
        List<dynamic> GetPerijinanUnApprovedForAdmin(int month, int year);
        List<dynamic> GetPerijinanUnApprovedForSupervisor(int supervisorId, int month, int year);
        List<Penalty> GetPenaltyByCurrentMonthYearAndSupervisorId(int supervisorId, DateTime fromDate, DateTime toDate);
        List<Penalty> GetAllPenaltyByCurrentMonthYear(DateTime fromDate, DateTime toDate);
        int GetMasaKerja(DateTime tanggalMasukKerja);
        decimal GetProsentasePremi(int masaKerjaDalamTahun);
    }
}