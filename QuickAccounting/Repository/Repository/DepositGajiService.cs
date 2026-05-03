using Microsoft.EntityFrameworkCore;
using QuickAccounting.Data;
using QuickAccounting.Data.HrPayroll;
using QuickAccounting.Repository.Interface;

namespace QuickAccounting.Repository.Repository
{
    public class DepositGajiService : IDepositGajiService
    {
        private readonly ApplicationDbContext _context;

        public DepositGajiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DepositGaji>> GetAllAsync()
        {
            return await _context.DepositGaji.OrderByDescending(x => x.DateCreated).ToListAsync();
        }

        public async Task<DepositGaji?> GetByIdAsync(int id)
        {
            return await _context.DepositGaji.FindAsync(id);
        }

        public async Task<int> CreateAsync(DepositGaji model)
        {
            model.DateCreated = DateTime.Now;
            await _context.DepositGaji.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.ID;
        }

        public async Task<bool> UpdateAsync(DepositGaji model)
        {
            var existing = await _context.DepositGaji.FindAsync(model.ID);
            if (existing == null) return false;

            existing.EmployeeID = model.EmployeeID;
            existing.DepositAmount = model.DepositAmount;
            existing.DateCreated = model.DateCreated;
            existing.CreatedBy = model.CreatedBy;

            _context.DepositGaji.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.DepositGaji.FindAsync(id);
            if (existing == null) return false;

            _context.DepositGaji.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<DepositGaji>> GetByEmployeeAsync(int employeeId)
        {
            return await _context.DepositGaji
                .Where(x => x.EmployeeID == employeeId)
                .OrderByDescending(x => x.DateCreated)
                .ToListAsync();
        }

        public async Task<List<DepositGaji>> GetByEmployeeLast12MonthsAsync(int employeeId)
        {
            var fromDate = DateTime.Now.AddMonths(-12);
            return await _context.DepositGaji
                .Where(x => x.EmployeeID == employeeId && x.DateCreated >= fromDate)
                .OrderByDescending(x => x.DateCreated)
                .ToListAsync();
        }

        public async Task<List<DepositGaji>> GetByEmployeeNotExpiredNotDisbursed(int employeeId)
        {
            return await _context.DepositGaji
                .Where(x => x.EmployeeID == employeeId && x.IsCredited == false && x.IsExpired == false)
                .OrderByDescending(x => x.DateCreated)
                .ToListAsync();
        }

        public async Task<List<DepositGaji>> GetByEmployeeExpiredNotDisbursed(int employeeId)
        {
            return await _context.DepositGaji
                .Where(x => x.EmployeeID == employeeId && x.IsCredited == false && x.IsExpired == true)
                .OrderByDescending(x => x.DateCreated)
                .ToListAsync();
        }

        public async Task<List<DepositGaji>> GetByEmployeeAndDisburseCode(int employeeId, string disburseCode)
        {
            return await _context.DepositGaji
                .Where(x => x.EmployeeID == employeeId && x.DisburseCode == disburseCode)
                .OrderByDescending(x => x.DateCreated)
                .ToListAsync();
        }

        /// <summary>
        /// Assigns the provided unique disburse code to all DepositGaji records for the employee
        /// that are not credited and not expired. Returns the number of records updated.
        /// </summary>
        public async Task<int> AssignDisburseCodeToPendingAsync(int employeeId, string uniqueDisburseCode)
        {
            // Load targeted records
            var pending = await _context.DepositGaji
                .Where(x => x.EmployeeID == employeeId && !x.IsCredited && !x.IsExpired)
                .ToListAsync();

            if (pending == null || pending.Count == 0)
                return 0;

            // Update the DisburseCode on each record
            foreach (var d in pending)
            {
                d.DisburseCode = uniqueDisburseCode;
            }

            // Persist changes
            await _context.SaveChangesAsync();

            return pending.Count;
        }
    }
}