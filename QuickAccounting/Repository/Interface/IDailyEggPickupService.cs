using QuickAccounting.Data.Recording;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickAccounting.Repository.Interface
{
    public interface IDailyEggPickupService
    {
        Task<IEnumerable<DailyEggPickup>> GetAllAsync();
        Task<DailyEggPickup> GetByIdAsync(int id);
        Task<DailyEggPickup> CreateAsync(DailyEggPickup dailyEggPickup);
        Task<DailyEggPickup> UpdateAsync(int id, DailyEggPickup dailyEggPickup);
        Task<bool> DeleteAsync(int id);

        // New Methods
        Task<IEnumerable<DailyEggPickup>> GetNotReceivedAsync();
        Task<DailyEggPickup> GetByCageNumberAndRecordDateAsync(string cageNumber, DateTime recordDate);
        Task<List<DailyEggPickup>> GetByCreatorIdAsync(int id);
    }
}
