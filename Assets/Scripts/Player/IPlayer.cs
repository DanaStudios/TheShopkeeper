using Transactions;

namespace Player
{
	public interface IPlayer : IBuyer, ISeller
	{
		bool PressedInteract { get; }
	}
}