using QuickAccounting.Data.Recording;
using QuickAccounting.Data;
using QuickAccounting.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace QuickAccounting.Constants
{
    public class StartingStockDate : IStartingStockDate
    {
        private readonly ApplicationDbContext _context;

        public StartingStockDate(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DateTime> Get()
        {
            var telurUtuh = await _context.StockTelurUtuh.FirstOrDefaultAsync();
            if(telurUtuh != null)
            {
                return telurUtuh.StockDate;
            }

            return new DateTime(1990,1,1);
        }

        public DateTime GetSync()
        {
            var telurUtuh = _context.StockTelurUtuh.FirstOrDefault();
            if (telurUtuh != null)
            {
                return telurUtuh.StockDate;
            }

            return new DateTime(1990, 1, 1);
        }

        public static DateTime StockDate = new DateTime(2025, 1, 29, 0, 0, 0);
    }

    public interface IStartingStockDate
    {
        Task<DateTime> Get();
        DateTime GetSync();
    }
}
