using Microsoft.EntityFrameworkCore;
using QuickAccounting.Data;
using QuickAccounting.Data.Inventory;
using QuickAccounting.Data.Recording;
using QuickAccounting.Repository.Interface;

namespace QuickAccounting.Repository.Repository
{
    public class StockAkhirService : IStockAkhirService
    {
        private readonly ApplicationDbContext _context;

        public StockAkhirService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StockOpnam>> GetAllAsync()
        {
            return await _context.Set<StockOpnam>().ToListAsync();
        }

        public async Task<StockOpnam?> GetByIdAsync(int id)
        {
            return await _context.Set<StockOpnam>().FindAsync(id);
        }

        public async Task<int> CreateAsync(StockOpnam model)
        {
            model.CreatedDate = DateTime.Now;
            _context.Set<StockOpnam>().Add(model);
            await _context.SaveChangesAsync();
            return model.ID;
        }

        public async Task CreateAllAsync(List<StockOpnam> model)
        {
            await _context.Set<StockOpnam>().AddRangeAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(StockOpnam model)
        {
            var existing = await _context.Set<StockOpnam>().FindAsync(model.ID);
            if (existing == null) return false;

            // update fields
            existing.ProductID = model.ProductID;
            existing.StockAkhir = model.StockAkhir;
            existing.StockDate = model.StockDate;
            existing.CreatedBy = model.CreatedBy;
            existing.GroupId = model.GroupId;
            // do not overwrite CreatedDate (preserve original)
            
            _context.Set<StockOpnam>().Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Set<StockOpnam>().FindAsync(id);
            if (existing == null) return false;
            _context.Set<StockOpnam>().Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<StockVOV>> GetReportStockVOV(DateTime startDate, DateTime endDate)
        {           
            var stockTotals = (from sp in _context.StockPosting
                               join p in _context.Product on sp.ProductId equals p.ProductId
                               join u in _context.Unit on p.UnitId equals u.UnitId
                               join pg in _context.ProductGroup on p.GroupId equals pg.GroupId

                               where p.GroupId == 7
                                  && sp.Date >= startDate
                                  && sp.Date <= endDate

                               // 2. Group ONLY the transactions first to avoid duplicate math
                               group sp by new
                               {
                                   sp.ProductId,
                                   p.ProductName,
                                   u.UnitName,
                                   pg.GroupName
                               } into g

                               orderby g.Key.ProductName

                               select new StockVOV
                               {
                                   // = 
                                   ProductId = g.Key.ProductId,
                                   ProductName = g.Key.ProductName,
                                   GroupName = g.Key.GroupName,
                                   UnitName = g.Key.UnitName,

                                   TotalInwardQty = g.Sum(x => x.InwardQty),
                                   TotalOutwardQty = g.Sum(x => x.OutwardQty),
                                   CurrentStock = g.Sum(x => x.InwardQty) - g.Sum(x => x.OutwardQty),

                                   // 3. Subquery to get the exact latest StockAkhir for this specific product
                                   StockReal = _context.StockOpnam
                                                  .Where(so => so.ProductID == g.Key.ProductId)
                                                  // NOTE: Change 'so.Date' to whatever your actual timestamp or ID column is
                                                  .OrderByDescending(x => x.StockDate)
                                                  .Select(so => (decimal?)so.StockAkhir)
                                                  .FirstOrDefault() ?? 0
                               }).ToList();

            return stockTotals;
        }

        public async Task<List<StockVOV>> GetReportStockPakan(DateTime startDate, DateTime endDate)
        {
            var stockTotals = (from sp in _context.StockPosting
                               join p in _context.Product on sp.ProductId equals p.ProductId
                               join u in _context.Unit on p.UnitId equals u.UnitId
                               join pg in _context.ProductGroup on p.GroupId equals pg.GroupId

                               where p.GroupId == 3
                                  && sp.Date >= startDate
                                  && sp.Date <= endDate

                               // 2. Group ONLY the transactions first to avoid duplicate math
                               group sp by new
                               {
                                   sp.ProductId,
                                   p.ProductName,
                                   u.UnitName,
                                   pg.GroupName
                               } into g

                               orderby g.Key.ProductName

                               select new StockVOV
                               {
                                   // = 
                                   ProductId = g.Key.ProductId,
                                   ProductName = g.Key.ProductName,
                                   GroupName = g.Key.GroupName,
                                   UnitName = g.Key.UnitName,

                                   TotalInwardQty = g.Sum(x => x.InwardQty),
                                   TotalOutwardQty = g.Sum(x => x.OutwardQty),
                                   CurrentStock = g.Sum(x => x.InwardQty) - g.Sum(x => x.OutwardQty),

                                   // 3. Subquery to get the exact latest StockAkhir for this specific product
                                   StockReal = _context.StockOpnam
                                                  .Where(so => so.ProductID == g.Key.ProductId)
                                                  // NOTE: Change 'so.Date' to whatever your actual timestamp or ID column is
                                                  .OrderByDescending(x => x.StockDate)
                                                  .Select(so => (decimal?)so.StockAkhir)
                                                  .FirstOrDefault() ?? 0
                               }).ToList();

            return stockTotals;
        }
    }

    public class StockVOV
    {
        //public DateTime Tanggal { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string GroupName { get; set; }
        public string UnitName { get; set; }
        public decimal TotalInwardQty { get; set; }
        public decimal TotalOutwardQty { get; set; }
        public decimal CurrentStock { get; set; }
        public decimal StockReal { get; set; }
    }
}