using AppData.Character;

namespace AppData.Shopkeeper
{
	public interface IShopkeeperData : ICharacterData
	{
		string DialogueText { get; }
		int StartingGold { get; }
	}
}