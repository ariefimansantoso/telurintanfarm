using QuickAccounting.Data;
using QuickAccounting.Data.HrPayroll;
using QuickAccounting.Repository.Interface;

namespace QuickAccounting.Repository.Repository
{
    public class HRServices : IHRServices
    {
        private readonly ApplicationDbContext _context;

        public HRServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Perijinan> PerijinanQuery()
        {
            return _context.Perijinan.AsQueryable();
        }

        public async Task<int> InsertPerijinan(Perijinan perijinan)
        {
            await _context.Perijinan.AddAsync(perijinan);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePerijinan(Perijinan model)
        {
            _context.Perijinan.Update(model);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePerijinan(Perijinan model)
        {
            _context.Perijinan.Remove(model);
            await _context.SaveChangesAsync();
            return true;
        }

        public IQueryable<Penalty> PenaltyQuery()
        {
            return _context.Penalty.AsQueryable();
        }

        public async Task<int> InsertPenalty(Penalty perijinan)
        {
            await _context.Penalty.AddAsync(perijinan);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePenalty(Penalty model)
        {
            _context.Penalty.Update(model);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePerijinan(Penalty model)
        {
            _context.Penalty.Remove(model);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
