using QuickAccounting.Data.Inventory;
using QuickAccounting.Data.Setting;

namespace QuickAccounting.Services
{
	public interface IPricingService
	{
		List<ProductView> GetAllProducts();
		List<PriceMaster> GetPrices();
		List<IGrouping<string, PriceMaster>> GetPricesTake5Days();
		void InsertPrice(List<PriceMaster> priceMasters);
		void UpdatePrices(List<PriceMaster> prices);
	}
}