using Items;

namespace Transactions
{
	public interface IBuyer
	{
		bool CanAfford(int totalCost);
		void Receive(IItem[] items);
		void Spend(int gold);
	}
}