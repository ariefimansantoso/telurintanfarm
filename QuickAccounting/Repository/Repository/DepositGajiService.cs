using Microsoft.EntityFrameworkCore;
using QuickAccounting.Data;
using QuickAccounting.Data.HrPayroll;
using QuickAccounting.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<int> AddAsync(DepositGaji model)
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

        public async Task<List<DepositGaji>> GetActiveDepositByEmployeeIdAsync(int employeeId)
        {
            return await _context.DepositGaji
                            .Where(d => d.EmployeeID == employeeId && !d.IsCredited)
                            .AsNoTracking()
                            .ToListAsync();
        }

        // marks all IsCredited == false rows in table as credited and stamps a single DisburseCode GUID
        public async Task<string> DisburseDepositAsync(string disburseCode = null)
        {
            var pending = await _context.DepositGaji.Where(d => !d.IsCredited).ToListAsync();
            if (pending == null || pending.Count == 0)
                return null;

            var code = string.IsNullOrWhiteSpace(disburseCode) ? Guid.NewGuid().ToString() : disburseCode;

            foreach (var d in pending)
            {
                d.IsCredited = true;
                d.DisburseCode = code;
                // optionally update DateCreated? We keep DateCreated as original creation date.
                _context.DepositGaji.Update(d);
            }

            await _context.SaveChangesAsync();
            return code;
        }
    }
}