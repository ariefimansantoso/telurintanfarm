using QuickAccounting.Data.HrPayroll;

namespace QuickAccounting.Repository.Interface
{
    public interface IHRServices
    {
        Task<bool> DeletePerijinan(Penalty model);
        Task<bool> DeletePerijinan(Perijinan model);
        Task InsertPenalty(Penalty perijinan);
        Task InsertPerijinan(Perijinan perijinan);
        IQueryable<Penalty> PenaltyQuery();
        IQueryable<Perijinan> PerijinanQuery();
        Task<bool> UpdatePenalty(Penalty model);
        Task<bool> UpdatePerijinan(Perijinan model);
    }
}