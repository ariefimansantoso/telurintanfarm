using DocumentFormat.OpenXml.Office2010.ExcelAc;
using QuickAccounting.Data;
using QuickAccounting.Data.Inventory;
using QuickAccounting.Data.Setting;

namespace QuickAccounting.Services
{

	public class PricingService : IPricingService
	{
		private readonly ApplicationDbContext _context;
		public PricingService(ApplicationDbContext context)
		{
			_context = context;
		}

		public List<PriceMaster> GetPrices()
		{
			return _context.PriceHistory.OrderByDescending(x => x.PriceDate).ToList();
		}

		public List<IGrouping<DateTime, PriceMaster>> GetPricesTake5Days()
		{
			if (_context.PriceHistory.Count() == 0)
				return null;

			return _context.PriceHistory.ToList().OrderByDescending(x => x.PriceDate).GroupBy(x => x.PriceDate).ToList();
		}

		public void InsertPrice(List<PriceMaster> priceMasters)
		{
			_context.PriceHistory.AddRange(priceMasters);
			_context.SaveChanges();
		}

		public void UpdatePrices(List<PriceMaster> prices)
		{
			_context.PriceHistory.UpdateRange(prices);
			_context.SaveChanges();
		}

		public List<ProductView> GetAllProducts()
		{
			var result = (from a in _context.Product
						  join b in _context.Brand on a.BrandId equals b.BrandId
						  join c in _context.ProductGroup on a.GroupId equals c.GroupId
						  join d in _context.Unit on a.UnitId equals d.UnitId
						  select new ProductView
						  {
							  ProductId = a.ProductId,
							  ProductCode = a.ProductCode,
							  ProductName = a.ProductName
						  }).OrderBy(x => Convert.ToInt32(x.ProductCode)).Take(4).ToList();
			return result;
		}
	}
}
