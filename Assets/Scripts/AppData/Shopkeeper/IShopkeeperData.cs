using AppData.Character;

namespace AppData.Shopkeeper
{
	public interface IShopkeeperData : ICharacterData
	{
		int StartingGold { get; }
	}
}