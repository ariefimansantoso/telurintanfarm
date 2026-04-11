using QuickAccounting.Data.Recording;
using QuickAccounting.Repository.Repository;

namespace QuickAccounting.Repository.Interface
{
    public interface IStockAkhirService
    {
        Task<List<StockOpnam>> GetAllAsync();
        Task<StockOpnam?> GetByIdAsync(int id);
        Task<int> CreateAsync(StockOpnam model);
        Task<bool> UpdateAsync(StockOpnam model);
        Task<bool> DeleteAsync(int id);
        Task CreateAllAsync(List<StockOpnam> model);
        Task<List<StockVOV>> GetReportStockVOV(DateTime startDate, DateTime endDate);
        Task<List<StockVOV>> GetReportStockPakan(DateTime startDate, DateTime endDate);
    }
}