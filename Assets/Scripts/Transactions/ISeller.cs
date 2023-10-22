using Items;

namespace Transactions
{
	public interface ISeller
	{
		IItem[] GetItems(IItem item, int count);
		void Earn(int gold);
	}
}