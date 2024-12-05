using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuickAccounting.Data;
using QuickAccounting.Data.Recording;
using QuickAccounting.Repository.Interface;

namespace QuickAccounting.Repository.Repository
{
    public class DailyEggPickupService : IDailyEggPickupService
    {
        private readonly ApplicationDbContext _context;

        public DailyEggPickupService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DailyEggPickup>> GetAllAsync()
        {
            return await _context.DailyEggPickup.ToListAsync();
        }

        public async Task<DailyEggPickup> GetByIdAsync(int id)
        {
            return await _context.DailyEggPickup.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<List<DailyEggPickup>> GetByCreatorIdAsync(int id)
        {
            return await _context.DailyEggPickup.Where(x => x.CreatedBy == id).ToListAsync();
        }

        public async Task<DailyEggPickup> CreateAsync(DailyEggPickup dailyEggPickup)
        {
            _context.DailyEggPickup.Add(dailyEggPickup);
            await _context.SaveChangesAsync();
            return dailyEggPickup;
        }

        public async Task<DailyEggPickup> UpdateAsync(int id, DailyEggPickup dailyEggPickup)
        {
            var existingRecord = await _context.DailyEggPickup.FindAsync(id);

            if (existingRecord == null)
            {
                throw new KeyNotFoundException("Record not found.");
            }

            // Update properties
            existingRecord.CageNumber = dailyEggPickup.CageNumber;
            existingRecord.StrainName = dailyEggPickup.StrainName;
            existingRecord.RecordDate = dailyEggPickup.RecordDate;
            existingRecord.TelurUtuhButir = dailyEggPickup.TelurUtuhButir;
            existingRecord.TelurUtuhKG = dailyEggPickup.TelurUtuhKG;
            existingRecord.TelurUtuhNettKG = dailyEggPickup.TelurUtuhNettKG;
            existingRecord.TelurBentesButir = dailyEggPickup.TelurBentesButir;
            existingRecord.TelurBentesKG = dailyEggPickup.TelurBentesKG;
            existingRecord.TelurBentesNettKG = dailyEggPickup.TelurBentesNettKG;
            existingRecord.TotalButir = dailyEggPickup.TotalButir;
            existingRecord.TotalNettKG = dailyEggPickup.TotalNettKG;
            existingRecord.ReceivedBy = dailyEggPickup.ReceivedBy;
            existingRecord.ReceivedOn = dailyEggPickup.ReceivedOn;

            await _context.SaveChangesAsync();
            return existingRecord;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingRecord = await _context.DailyEggPickup.FindAsync(id);

            if (existingRecord == null)
            {
                throw new KeyNotFoundException("Record not found.");
            }

            _context.DailyEggPickup.Remove(existingRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        // New Method: GetNotReceivedAsync
        public async Task<IEnumerable<DailyEggPickup>> GetNotReceivedAsync()
        {
            return await _context.DailyEggPickup
                .Where(x => x.ReceivedBy < 0)
                .ToListAsync();
        }

        // New Method: GetByCageNumberAndRecordDateAsync
        public async Task<DailyEggPickup> GetByCageNumberAndRecordDateAsync(string cageNumber, DateTime recordDate)
        {
            return await _context.DailyEggPickup
                .FirstOrDefaultAsync(x => x.CageNumber == cageNumber && x.RecordDate == recordDate);
        }
    }
}
