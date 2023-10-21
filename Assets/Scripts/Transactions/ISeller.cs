using Inventory;
using Item;

namespace Transactions
{
	public interface ISeller
	{
		void SellTo(IItem item, int count, IInventory inventory);
	}
}