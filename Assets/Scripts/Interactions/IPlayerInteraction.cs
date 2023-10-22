using Shopkeeper;

namespace Interactions
{
	public interface IPlayerInteraction
	{
		bool IsInteracting { get; }
		IShopkeeper LastInteracted { get; }
	}
}