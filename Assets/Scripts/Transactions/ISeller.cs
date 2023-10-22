using Inventory;
using Items;

namespace Transactions
{
	public interface ISeller
	{
		void SellTo(IItem item, int count, IInventory inventory);
	}
}