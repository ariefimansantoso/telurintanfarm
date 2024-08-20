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
            return _context.Perijinans.AsQueryable();
        }

        public async Task InsertPerijinan(Perijinan perijinan)
        {
            await _context.Perijinans.AddAsync(perijinan);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePerijinan(Perijinan model)
        {
            _context.Perijinans.Update(model);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePerijinan(Perijinan model)
        {
            _context.Perijinans.Remove(model);
            await _context.SaveChangesAsync();
            return true;
        }

        public IQueryable<Penalty> PenaltyQuery()
        {
            return _context.Penalties.AsQueryable();
        }

        public async Task InsertPenalty(Penalty perijinan)
        {
            await _context.Penalties.AddAsync(perijinan);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePenalty(Penalty model)
        {
            _context.Penalties.Update(model);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePerijinan(Penalty model)
        {
            _context.Penalties.Remove(model);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
