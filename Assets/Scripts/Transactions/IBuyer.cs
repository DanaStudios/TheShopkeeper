using Items;

namespace Transactions
{
	public interface IBuyer
	{
		bool CanAfford(int totalCost);
		void ReceiveItems(IItem[] items);
		void Spend(int gold);
	}
}