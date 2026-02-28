using Microsoft.EntityFrameworkCore;
using QuickAccounting.Data;
using QuickAccounting.Data.HrPayroll;
using QuickAccounting.Repository.Interface;

namespace QuickAccounting.Repository.Repository
{
    public class SuratPeringatanService : ISuratPeringatan
    {
        private readonly ApplicationDbContext _context;

        public SuratPeringatanService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all Surat Peringatan records that are not deleted
        /// </summary>
        public async Task<List<SuratPeringatan>> GetAll()
        {
            return await _context.SuratPeringatan
                .Where(sp => !sp.IsDeleted)
                .ToListAsync();
        }

        /// <summary>
        /// Get Surat Peringatan by ID
        /// </summary>
        public async Task<SuratPeringatan> GetByID(int id)
        {
            return await _context.SuratPeringatan
                .FirstOrDefaultAsync(sp => sp.Id == id && !sp.IsDeleted);
        }

        /// <summary>
        /// Get all Surat Peringatan records for a specific employee
        /// </summary>
        public async Task<List<SuratPeringatan>> GetByEmployeeID(int employeeId)
        {
            return await _context.SuratPeringatan
                .Where(sp => sp.EmployeeId == employeeId && !sp.IsDeleted)
                .OrderByDescending(sp => sp.DateCreated)
                .ToListAsync();
        }

        /// <summary>
        /// Insert a new Surat Peringatan record
        /// </summary>
        public async Task<int> Insert(SuratPeringatan model)
        {
            model.DateCreated = DateTime.Now;
            model.DateModified = DateTime.Now;
            model.IsDeleted = false;

            await _context.SuratPeringatan.AddAsync(model);
            await _context.SaveChangesAsync();

            return model.Id;
        }

        /// <summary>
        /// Update an existing Surat Peringatan record
        /// </summary>
        public async Task<bool> Update(SuratPeringatan model)
        {
            var existingRecord = await _context.SuratPeringatan
                .FirstOrDefaultAsync(sp => sp.Id == model.Id);

            if (existingRecord == null)
                return false;

            existingRecord.EmployeeId = model.EmployeeId;
            existingRecord.SpNumber = model.SpNumber;
            existingRecord.DateModified = DateTime.Now;
            existingRecord.ModifiedBy = model.ModifiedBy;
            existingRecord.SpDescription = model.SpDescription;
            existingRecord.SPDate = model.SPDate;
            existingRecord.SPOfficialNumber = model.SPOfficialNumber;

            _context.SuratPeringatan.Update(existingRecord);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Soft delete a Surat Peringatan record by setting IsDeleted flag
        /// </summary>
        public async Task<bool> SetDeleted(int id)
        {
            var record = await _context.SuratPeringatan
                .FirstOrDefaultAsync(sp => sp.Id == id);

            if (record == null)
                return false;

            record.IsDeleted = true;
            record.DateModified = DateTime.Now;

            _context.SuratPeringatan.Update(record);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}