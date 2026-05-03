using Microsoft.EntityFrameworkCore;
using QuickAccounting.Data;
using QuickAccounting.Data.HrPayroll;
using QuickAccounting.Repository.Interface;

namespace QuickAccounting.Repository.Repository
{
    public class KontrakKerjaService : IKontrakKerjaService
    {
        private readonly ApplicationDbContext _context;

        public KontrakKerjaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<KontrakKerja>> GetAllAsync()
        {
            return await _context.KontrakKerja.OrderByDescending(x => x.DateCreated).ToListAsync();
        }

        public async Task<KontrakKerja?> GetByIdAsync(int id)
        {
            return await _context.KontrakKerja.FindAsync(id);
        }

        public async Task<int> CreateAsync(KontrakKerja model)
        {
            model.DateCreated = DateTime.Now;
            await _context.KontrakKerja.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.ID;
        }

        public async Task<bool> UpdateAsync(KontrakKerja model)
        {
            var existing = await _context.KontrakKerja.FindAsync(model.ID);
            if (existing == null) return false;

            existing.DateStart = model.DateStart;
            existing.DateEnd = model.DateEnd;
            existing.EmployeeID = model.EmployeeID;
            existing.ContractDetails = model.ContractDetails;
            existing.DateModified = DateTime.Now;
            existing.ModifiedBy = model.ModifiedBy;
            existing.IsExpired = model.IsExpired;
            existing.IsPermanent = model.IsPermanent;

            _context.KontrakKerja.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.KontrakKerja.FindAsync(id);
            if (existing == null) return false;

            _context.KontrakKerja.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<KontrakKerja>> GetContractByEmployeeIDAsync(int employeeId)
        {
            return await _context.KontrakKerja
                .Where(x => x.EmployeeID == employeeId)
                .OrderByDescending(x => x.DateStart)
                .ToListAsync();
        }

        public async Task<List<KontrakKerja>> GetExpiredContractsAsync()
        {
            var today = DateTime.Now.Date;
            return await _context.KontrakKerja
                .Where(x => x.IsExpired == true && x.DateEnd.Date < today)
                .OrderByDescending(x => x.DateEnd)
                .ToListAsync();
        }

        public async Task<KontrakKerja?> GetActiveContractByEmployeeIDAsync(int employeeId)
        {
            var today = DateTime.Now.Date;
            return await _context.KontrakKerja
                .Where(x => x.EmployeeID == employeeId &&
                            !x.IsExpired &&
                            (x.IsPermanent || x.DateEnd.Date >= today))
                .OrderByDescending(x => x.DateStart)
                .FirstOrDefaultAsync();
        }

        public async Task<KontrakKerja> GetUnSignedContractsAsync(int employeeID)
        {
            return await _context.KontrakKerja
                .Where(x => !x.IsSigned && x.EmployeeID == employeeID).FirstOrDefaultAsync();
        }

        public async Task<KontrakKerja> GetLatestSignedContractsAsync(int employeeID)
        {
            return await _context.KontrakKerja
                .Where(x => x.IsSigned && x.EmployeeID == employeeID)
                .OrderByDescending(x => x.DateCreated)
                .FirstOrDefaultAsync();
        }
    }
}